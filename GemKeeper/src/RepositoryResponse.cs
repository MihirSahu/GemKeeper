class RepositoryResponse {
  public bool isSuccessful;
  public object? data;
  public string message;

  public RepositoryResponse(bool isSuccessful, string message, object? data = null) {
    this.isSuccessful = isSuccessful;
    this.data = data;
    this.message = message;
  }
}