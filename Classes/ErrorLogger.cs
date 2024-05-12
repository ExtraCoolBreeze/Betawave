using System;
using System.IO;

using Betawave.Classes;
public class Logger
{
    private string LoggingFile;

    public Logger(string loggingFile)
    {
        LoggingFile = loggingFile;
    }

    public void Log(string message)
    {
        // Only log to file
        File.AppendAllText(LoggingFile, $"{DateTime.Now}: {message}\n");
    }

    public void LogError(Exception ex)
    {
        Log($"Error: {ex.Message}");
    }
}
