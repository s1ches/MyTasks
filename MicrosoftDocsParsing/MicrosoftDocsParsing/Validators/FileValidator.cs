using DocumentFormat.OpenXml.Packaging;
using MicrosoftDocsParsing.ResultMessanger;
using MicrosoftDocsParsing.Services.FileProcessor.Extensions;

namespace MicrosoftDocsParsing.Validators;
/// <summary>
/// FileValidator отвечает за валидацию файла
/// </summary>
public class FileValidator : IValidator
{
    /// <summary>
    /// Проверка на то, что файл существует, с нужным расширением, пустой ли он, можно ли его открыть
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <returns>Результат валидации FileProcessResult</returns>
    public static Task<string> ValidateAsync(string filePath)
    {
        if (!File.Exists(filePath))
            return Task.FromResult(FileProcessResult.FileNotFound(filePath));
        
        if (!filePath.IsAcceptedFileExtension())
            return Task.FromResult(FileProcessResult.WrongFileFormat(filePath.GetFileExtensionText()));

        if (IsEmptyFile(filePath))
            return Task.FromResult(FileProcessResult.EmptyFile(filePath));

        if (!CanOpenFile(filePath))
            return Task.FromResult(FileProcessResult.CannotOpen(filePath));

        return Task.FromResult(FileProcessResult.Success);
    }

    private static bool IsEmptyFile(string filePath) => new FileInfo(filePath).Length == 0;

    private static bool CanOpenFile(string filePath)
    {
        try
        {
            using var document = WordprocessingDocument.Open(filePath, isEditable: false);
            return true;
        }
        catch (IOException ex)
        {
            return false;
        }
    }
}