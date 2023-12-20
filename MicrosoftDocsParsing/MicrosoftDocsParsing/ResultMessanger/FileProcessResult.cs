namespace MicrosoftDocsParsing.ResultMessanger;

/// <summary>
/// Результаты обработки файла
/// </summary>
public static class FileProcessResult
{
    public static string Success => "Ok";

    public static string SuccessProcess(string filePath) => $"{Success}: {filePath}";

    public static string CannotOpen(string filePath) => $"File {filePath} already opened on this machine";
    
    public static string DirectoryNotFound(string directoryPath) => $"Directory {directoryPath} not found";

    public static string DirectoryHasNoAcceptedFiles(string directoryPath) =>
        $"Directory {directoryPath} doesn't contains files with need extension";

    public static string EmptyDirectory(string directoryPath) => $"Directoy {directoryPath} is empty";
    
    public static string FileNotFound(string filePath) => $"File {filePath} not found";
    
    public static string EmptyFile(string filePath) => $"File {filePath} is empty";
    
    public static string WrongFileFormat(string fileFormat) => $"Wrong file format: {fileFormat}";

    public static bool IsSuccess(string result) => result.StartsWith(Success);
}