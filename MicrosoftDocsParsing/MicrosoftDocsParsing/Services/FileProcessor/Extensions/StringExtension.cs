namespace MicrosoftDocsParsing.Services.FileProcessor.Extensions;

public static class StringExtension
{
    /// <summary>
    /// Разрешённые расширения
    /// </summary>
    private static readonly Dictionary<string, FileExtension> AcceptedFileExtensions;

    static StringExtension()
    {
        AcceptedFileExtensions = new Dictionary<string, FileExtension>
        {
            [".doc"] = FileExtension.Doc,
            [".docx"] = FileExtension.Docx
        };
    }
    
    /// <summary>
    /// Является ли расширение файла допустимым или нет
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <returns>true - если расширение .doc или .docx, false - иначе</returns>
    public static bool IsAcceptedFileExtension(this string filePath)
    {
        var fileExtension = filePath.GetFileExtensionText();
        return AcceptedFileExtensions.TryGetValue(fileExtension, out var _);
    }

    /// <summary>
    /// Расширение файла
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    public static string GetFileExtensionText(this string filePath) => new FileInfo(filePath).Extension;

    /// <summary>
    /// Путь к файлу без его расширения
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <returns></returns>
    public static string WithoutExtension(this string filePath) =>
        filePath.Split(filePath.GetFileExtensionText()).First();

    /// <summary>
    /// Имя файла
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    public static string GetFileName(this string filePath) => new FileInfo(filePath).Name;
}