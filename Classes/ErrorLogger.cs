using Betawave.Classes;
public class ErrorLogger
{
    //declaring variables
    private string LoggingFile;

    //Calling constructor
    public ErrorLogger(string loggingFile)
    {
        this.LoggingFile = loggingFile;
    }

    /// <summary>
    /// When called and passed a string this method writes the string to the BetawaveErrorLog.txt file
    /// along with the data and time.
    /// </summary>
    /// <param name="message"></param>
    public void Log(string message)
    {
        File.AppendAllText(LoggingFile, $"{DateTime.Now}: {message}\n");
    }

    /// <summary>
    /// When this function is called it calls the log method
    /// </summary>
    /// <param name="ex"></param>
    public void LogError(Exception ex)
    {
        Log($"Error: {ex.Message}");
    }
}
