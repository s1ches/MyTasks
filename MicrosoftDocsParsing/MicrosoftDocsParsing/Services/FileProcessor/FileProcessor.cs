using DocumentFormat.OpenXml.Packaging;
using MicrosoftDocsParsing.ResultMessanger;
using MicrosoftDocsParsing.Services.FileProcessor.Extensions;
using MicrosoftDocsParsing.Validators;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;

namespace MicrosoftDocsParsing.Services.FileProcessor;

/// <summary>
/// FileProcessor отвечает за парсинг .doc и .docx файлов и запись их в txt файлы
/// </summary>
public class FileProcessor : IFileProcessor
{
    /// <summary>
    /// Настрокйки проекта
    /// </summary>
    private static readonly AppSettings Settings = AppSettings.Instance;
    
    /// <summary>
    /// Парсинг и запись всех файлов из папки UnparsedFiles(запись в папку ParsedFiles) 
    /// </summary>
    /// <returns>Коллекция результатов обработки файлов FileProcessResult</returns>
    public static async Task<IEnumerable<string>> ProcesAllUnparsedFilesAsync()
    {
        var directoryPath = @$"{Settings.FileDirectory}\{Settings.UnparsedFiles}";
        
        var directoryValidationResult = await DirectoryValidator.ValidateAsync(directoryPath);
        if (directoryValidationResult != FileProcessResult.Success)
            return new[] { directoryValidationResult };
        
        var filePaths = Directory.EnumerateFiles(directoryPath);

        var fileProcessingTasks = new Dictionary<string, Task<string>>();
        
        foreach (var filePath in filePaths)
            fileProcessingTasks[filePath] = 
                Task.Run(async () => await ProcessFileAsync($"{filePath}"));

        return await Task.WhenAll(fileProcessingTasks.Values);
    }
    
    /// <summary>
    /// Парсинг и запись файла в папку ParsedFiles
    /// </summary>
    /// <param name="filePath">Путь к файлу .doc или .docx</param>
    /// <returns>Результат обработки файла FileProcessResult</returns>
    public static async Task<string> ProcessFileAsync(string filePath)
    {
        var validationResult = await FileValidator.ValidateAsync(filePath);
        
        if (validationResult != FileProcessResult.Success)
            return validationResult;
        
        var destinationFilePath =
            @$"{Settings.FileDirectory}\{Settings.ParsedFiles}\{filePath.GetFileName().WithoutExtension()}.txt";

        var result = FormResultTextAsync(filePath);
        
        await File.WriteAllLinesAsync(destinationFilePath, result.ToBlockingEnumerable());
        
        return FileProcessResult.SuccessProcess(filePath);
    }
    
    /// <summary>
    /// Формирование коллеции параграфов, удаление скрытых символов из них
    /// </summary>
    /// <param name="filePath">Путь к файлу .doc или .docx</param>
    /// <returns>Коллекция распаршенных параграфов</returns>
    private static async IAsyncEnumerable<string> FormResultTextAsync(string filePath)
    {
        using var document = WordprocessingDocument.Open(filePath, isEditable: false);

        if (document.MainDocumentPart?.Document.Body is null)
            yield break;

        var paragraphs = document.MainDocumentPart.Document.Body.Descendants<Paragraph>();
        var processParagraphTasks = new List<Task<string>>();
        
        foreach (var paragraph in paragraphs)
            processParagraphTasks.Append(Task.Run(() => paragraph.TextWithoutHiddenElements()));
        
        foreach (var task in processParagraphTasks)
            yield return await task;
    }
}