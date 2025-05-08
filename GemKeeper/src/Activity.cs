public class SafeHtmlItem {
  public string html {get; set;}
}

class Activity
{
  public string header {get; set;}
  public string title {get; set;}
  public string time {get; set;}
  public string[] products {get; set;}
  public string[] activityControls {get; set;}
  public List<SafeHtmlItem> safeHtmlItem {get; set;}

  public RepositoryResponse GetParsedDateTime() {
    try {
      return new RepositoryResponse(isSuccessful: true, data: DateTime.Parse(time), message: "Datetime parsed successfully.");
    }
    catch (Exception error) {
      return new RepositoryResponse(isSuccessful: false, message: error.Message);
    }
  }
}