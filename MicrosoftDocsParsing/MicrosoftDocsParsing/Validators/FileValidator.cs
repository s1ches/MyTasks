using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MicrosoftDocsParsing.ResultMessanger;
using MicrosoftDocsParsing.Services.FileProcessor.Extensions;

namespace MicrosoftDocsParsing.Validators;

public class FileValidator : IValidator
{
    public static Task<string> ValidateAsync(string filePath)
    {
        if (!File.Exists(filePath))
            return Task.FromResult(FileProcessResult.FileNotFound(filePath));
        
        if (!filePath.IsAcceptedFileExtension())
            return Task.FromResult(FileProcessResult.WrongFileFormat(filePath.GetFileExtensionText()));

        if (IsEmptyFile(filePath))
            return Task.FromResult(FileProcessResult.EmptyFile(filePath));

        return Task.FromResult(FileProcessResult.Success);
    }

    private static bool IsEmptyFile(string filePath) => new FileInfo(filePath).Length == 0;
}