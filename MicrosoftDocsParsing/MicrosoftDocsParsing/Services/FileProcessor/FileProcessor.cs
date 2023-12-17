using System.IO.Packaging;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Packaging;
using MicrosoftDocsParsing.ResultMessanger;
using MicrosoftDocsParsing.Services.FileProcessor.Extensions;
using MicrosoftDocsParsing.Validators;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;

namespace MicrosoftDocsParsing.Services.FileProcessor;

public class FileProcessor : IFileProcessor
{
    private static readonly AppSettings Settings = AppSettings.Instance; 
    
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
    
    public static async Task<string> ProcessFileAsync(string filePath)
    {
        var validationResult = await FileValidator.ValidateAsync(filePath);
        
        if (validationResult != FileProcessResult.Success)
            return validationResult;

        using var document = WordprocessingDocument.Open(filePath, isEditable: false);

        var destinationFilePath =
            @$"{Settings.FileDirectory}\{Settings.ParsedFiles}\{filePath.GetFileName().WithoutExtension()}.txt";
        
        var text = document.MainDocumentPart!.RootElement!.Descendants<Paragraph>();
        
        await File.WriteAllLinesAsync(destinationFilePath, text.Select(paragraph => paragraph.InnerText));
        
        return FileProcessResult.SuccessProcess(filePath);
    }
}