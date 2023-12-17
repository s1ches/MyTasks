namespace MicrosoftDocsParsing.Services.FileProcessor.Extensions;

public static class StringExtension
{
    private static readonly Dictionary<string, FileExtension> AcceptedFileExtensions;

    static StringExtension()
    {
        AcceptedFileExtensions = new Dictionary<string, FileExtension>
        {
            [".doc"] = FileExtension.Doc,
            [".docx"] = FileExtension.Docx
        };
    }
    
    public static bool IsAcceptedFileExtension(this string filePath)
    {
        var fileExtension = filePath.GetFileExtensionText();
        return AcceptedFileExtensions.TryGetValue(fileExtension, out var _);
    }

    public static string GetFileExtensionText(this string filePath) => new FileInfo(filePath).Extension;

    public static string WithoutExtension(this string filePath) =>
        filePath.Split(filePath.GetFileExtensionText()).First();

    public static string GetFileName(this string filePath) => new FileInfo(filePath).Name;

    public static string WithoutInvisibleText(this string text)
    {
        return default!;
    }
}