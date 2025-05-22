public class SafeHtmlItem
{
  public string html { get; set; }
}

class Activity
{
  public string header { get; set; }
  public string title { get; set; }
  public string time { get; set; }
  public string[] products { get; set; }
  public string[] activityControls { get; set; }
  public List<SafeHtmlItem> safeHtmlItem { get; set; }

  public RepositoryResponse GetParsedDateTime()
  {
    try
    {
      return new RepositoryResponse(isSuccessful: true, data: DateTimeOffset.Parse(time), message: "Datetime parsed successfully.");
    }
    catch (Exception error)
    {
      return new RepositoryResponse(isSuccessful: false, message: error.Message);
    }
  }

  public RepositoryResponse GetFormattedOutput()
  {
    try {
      var converter = new ReverseMarkdown.Converter();

      string header = $"---\nTime: \"{this.time}\"\n---";
      string prompt = $"## You\n> {this.title}\n";
      string chatMarkdown = converter.Convert(this.safeHtmlItem.FirstOrDefault()?.html ?? "");

      return new RepositoryResponse(isSuccessful: true, data: header + "\n" + prompt + "## Gemini\n" + chatMarkdown, message: "Activity data formatted successfully.");
    }
    catch {
      return new RepositoryResponse(isSuccessful: false, message: $"The data for the chat with prompt {this.title} could not be formatted successfully.");
    }
  }
}