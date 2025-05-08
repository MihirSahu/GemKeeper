class Validate
{
  public static RepositoryResponse ValidateFileExists(string FilePath)
  {
    bool fileExists = File.Exists(FilePath);
    if (fileExists)
    {
      return new RepositoryResponse(isSuccessful: true, message: "The file exists.");
    }
    return new RepositoryResponse(isSuccessful: false, message: $"The file '{FilePath}' doesn't exist.");
  }

  public static RepositoryResponse ValidateDirectoryExists(string DirectoryPath)
  {
    bool directoryExists = Directory.Exists(DirectoryPath);
    if (directoryExists)
    {
      return new RepositoryResponse(isSuccessful: true, message: "The directory exists.");
    }
    return new RepositoryResponse(isSuccessful: false, message: $"The directory '{DirectoryPath}' doesn't exist.");
  }
}