using MicrosoftDocsParsing.ResultMessanger;
using MicrosoftDocsParsing.Services.FileProcessor.Extensions;

namespace MicrosoftDocsParsing.Validators;

/// <summary>
/// DirectoryValidator отвечает за валидацию директории с файлами
/// </summary>
public class DirectoryValidator : IValidator
{
    /// <summary>
    /// Проверка на то, что директория существует, там есть файлы и среди них есть те, что с подходящим расширением
    /// </summary>
    /// <param name="directoryPath">Путь к директории</param>
    /// <returns>Результат валидации FileProcessResult</returns>
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