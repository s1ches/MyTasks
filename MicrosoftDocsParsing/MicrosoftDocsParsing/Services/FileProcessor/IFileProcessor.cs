namespace MicrosoftDocsParsing.Services.FileProcessor;

public interface IFileProcessor
{
    public static abstract Task<string> ProcessFileAsync(string filePath);
}