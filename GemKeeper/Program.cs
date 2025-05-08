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
      Console.WriteLine(options.FilePath);
      Console.WriteLine(options.OutputPath);

      RepositoryResponse extractFileResponse = Extract.ExtractFile(options.FilePath);
      if (!extractFileResponse.isSuccessful) throw new Exception(extractFileResponse.message);

      string? directoryPath = Path.GetDirectoryName(options.FilePath);
      string? activityFilePath = Path.Combine(directoryPath, "Takeout", "My Activity", "Gemini Apps", "MyActivity.json");
      RepositoryResponse validateActivityPathResponse = Validate.ValidateFileExists(activityFilePath);
      if (!validateActivityPathResponse.isSuccessful) throw new Exception(validateActivityPathResponse.message);

      string myActivityJsonString = File.ReadAllText(activityFilePath);
      List<Activity>? activities = JsonSerializer.Deserialize<List<Activity>>(myActivityJsonString) ?? throw new Exception("MyActivity.json could not be deserialized.");

      foreach (var activity in activities) {
        activity.title = activity.title.Substring(activity.title.IndexOf(" ") + 1);
      }

      RepositoryResponse validateOutputDirectory = Validate.ValidateDirectoryExists(options.OutputPath);
      if (!validateOutputDirectory.isSuccessful) {
        Directory.CreateDirectory(options.OutputPath);
      }

      foreach (var activity in activities) {
        RepositoryResponse activityDateTime = activity.GetParsedDateTime();
        if (!activityDateTime.isSuccessful) throw new Exception($"The date/time for the chat with prompt {activity.title} could not be parsed successfully.");
        string outputFilePath = Path.Combine(options.OutputPath, ((DateTime)activityDateTime.data).ToString("yyyy-MM-dd-ss") + ".md");

        RepositoryResponse formattedOutput = activity.GetFormattedOutput();
        if (!formattedOutput.isSuccessful) throw new Exception(formattedOutput.message);

        File.WriteAllText(outputFilePath, (string)formattedOutput.data);
        if (!Validate.ValidateFileExists(outputFilePath).isSuccessful) throw new Exception("Files cannot be created in the specified output folder.");
      }
    }
    catch (Exception error)
    {
      Console.WriteLine(error.Message);
    }
  }
}
