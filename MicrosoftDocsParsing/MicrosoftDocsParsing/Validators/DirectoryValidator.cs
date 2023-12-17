using MicrosoftDocsParsing.ResultMessanger;
using MicrosoftDocsParsing.Services.FileProcessor.Extensions;

namespace MicrosoftDocsParsing.Validators;

public class DirectoryValidator : IValidator
{
    public static Task<string> ValidateAsync(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            return Task.FromResult(FileProcessResult.DirectoryNotFound(directoryPath));

        if (!Directory.EnumerateFiles(directoryPath).Any())
            return Task.FromResult(FileProcessResult.EmptyDirectory(directoryPath));

        if (!Directory.EnumerateFiles(directoryPath).Any(file => file.IsAcceptedFileExtension()))
            return Task.FromResult(FileProcessResult.DirectoryHasNoAcceptedFiles(directoryPath));
        
        return Task.FromResult(FileProcessResult.Success);
    }
}