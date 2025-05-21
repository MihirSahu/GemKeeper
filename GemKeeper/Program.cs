using System.Text.Json;
using CommandLine;
using GemKeeper;

class Program
{
  static void Main(string[] args)
  {
    var parser = new CommandLine.Parser(with => with.HelpWriter = null);
    var parserResult = parser.ParseArguments<Options>(args);
    parserResult
      .WithParsed<Options>(options =>
      {
        Run(options);
      })
      .WithNotParsed(errs => Options.DisplayHelp(parserResult));
  }

  public static void Run(Options options)
  {
    try
    {
      Logger.Log($"Google Takeout file path: {options.FilePath}", options.Verbose);
      Logger.Log($"Output path: {options.OutputPath}", options.Verbose);

      RepositoryResponse extractFileResponse = Extract.ExtractFile(options.FilePath);
      if (!extractFileResponse.isSuccessful) throw new Exception(extractFileResponse.message);
      Logger.Log(extractFileResponse.message, options.Verbose);

      string? directoryPath = Path.GetDirectoryName(options.FilePath);
      string? activityFilePath = Path.Combine(directoryPath, "Takeout", "My Activity", "Gemini Apps", "MyActivity.json");
      RepositoryResponse validateActivityPathResponse = Validate.ValidateFileExists(activityFilePath);
      if (!validateActivityPathResponse.isSuccessful) throw new Exception(validateActivityPathResponse.message);
      Logger.Log(validateActivityPathResponse.message, options.Verbose);

      string myActivityJsonString = File.ReadAllText(activityFilePath);
      List<Activity>? activities = JsonSerializer.Deserialize<List<Activity>>(myActivityJsonString) ?? throw new Exception("MyActivity.json could not be deserialized.");
      Logger.Log("MyActivity.json data deserialized successfully.", options.Verbose);

      foreach (var activity in activities) {
        activity.title = activity.title.Substring(activity.title.IndexOf(" ") + 1);
      }
      Logger.Log("Prompts processed successfully.", options.Verbose);

      RepositoryResponse validateOutputDirectory = Validate.ValidateDirectoryExists(options.OutputPath);
      if (!validateOutputDirectory.isSuccessful) {
        Directory.CreateDirectory(options.OutputPath);
        Logger.Log("", options.Verbose);
      }

      foreach (var activity in activities) {
        RepositoryResponse activityDateTime = activity.GetParsedDateTime();
        string formattedDateTime = ((DateTime)activityDateTime.data).ToString("yyyy-MM-dd-ss");

        try
        {
          string outputFilePath = Path.Combine(options.OutputPath, formattedDateTime + ".md");

          RepositoryResponse formattedOutput = activity.GetFormattedOutput();
          if (!formattedOutput.isSuccessful) throw new Exception(formattedOutput.message);

          File.WriteAllText(outputFilePath, (string)formattedOutput.data);
          if (!Validate.ValidateFileExists(outputFilePath).isSuccessful) throw new Exception("Files cannot be created in the specified output folder.");
          Logger.Log(formattedDateTime + " ✓", options.Verbose);
        }
        catch (Exception error)
        {
          Logger.Log(formattedDateTime + " x", options.Verbose);
          Logger.Log(error.Message, options.Verbose);
        }
      }

      Console.WriteLine($"{activities.Count} activities processed.");
      Console.WriteLine($"Chat history exported to {options.OutputPath}.");
    }
    catch (Exception error)
    {
      Console.WriteLine(error.Message);
      Console.WriteLine("Exiting the program.");
    }
  }
}
