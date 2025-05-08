using System.IO.Compression;

namespace GemKeeper
{

  class Extract
  {
    public static RepositoryResponse ExtractFile(string FilePath)
    {
      try
      {
        RepositoryResponse validateFilePathExistsResponse = Validate.ValidateFileExists(FilePath);
        if (!validateFilePathExistsResponse.isSuccessful) throw new Exception(validateFilePathExistsResponse.message);

        string? directoryName = Path.GetDirectoryName(FilePath) ?? throw new Exception("File is in root or directory is invalid. Move the file to another location and try again.");

        ZipFile.ExtractToDirectory(FilePath, Path.GetDirectoryName(FilePath));

        return new RepositoryResponse(isSuccessful: true, message: "The extraction process was successful.");
      }
      catch (Exception error)
      {
        return new RepositoryResponse(isSuccessful: false, data: error, message: error.Message);
      }
    }
  }
}