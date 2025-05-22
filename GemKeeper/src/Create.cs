using System.IO;

class Create
{
  public static RepositoryResponse CreateDirectory(string DirectoryPath)
  {
    try
    {
      RepositoryResponse validateYearFolder = Validate.ValidateDirectoryExists(DirectoryPath);
      if (!validateYearFolder.isSuccessful) {
        Directory.CreateDirectory(DirectoryPath);
        return new RepositoryResponse(isSuccessful: true, message: "Directory created successfully");
      }
      return new RepositoryResponse(isSuccessful: false, message: "Directory already exists.");
    }
    catch (Exception error)
    {
      return new RepositoryResponse(isSuccessful: false, data: error, message: error.Message);
    }
  }
}