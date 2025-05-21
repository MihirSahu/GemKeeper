class Logger {
  public static void Log(string message, bool isVerbose) {
    if (isVerbose) Console.WriteLine(message);
  }
}