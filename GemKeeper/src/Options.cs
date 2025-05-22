using CommandLine;
using CommandLine.Text;

namespace GemKeeper
{
  public class Options
  {
    [Usage(ApplicationAlias = "gemkeeper")]
    public static IEnumerable<Example> Examples
    {
      get
      {
        return new List<Example>() {
        new Example("Export Gemini chat history to markdown", new Options { FilePath = "./takeout.zip", OutputPath = "gemini_output/" })
      };
      }
    }
    [Option('f', "file-path", Required = true, HelpText = "Specify the activity file path.")]
    public string? FilePath { get; set; }

    [Option('o', "output", Required = true, HelpText = "Specify the output folder.")]
    public string? OutputPath { get; set; }

    [Option('a', "arrange", Required = false, HelpText = "Organize files by year and month.")]
    public bool Arrange { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Print additional logs.")]
    public bool Verbose { get; set; }

    public static void DisplayHelp<T>(ParserResult<T> result)
    {
      var helpText = HelpText.AutoBuild(result, h =>
      {
        h.AdditionalNewLineAfterOption = false;
        h.Heading = "Gemkeeper v1.0.0";
        h.Copyright = "Made with <3 by Mihir Sahu";
        return HelpText.DefaultParsingErrorsHandler(result, h);
      }, e => e);
      Console.WriteLine(helpText);
    }
  };

}