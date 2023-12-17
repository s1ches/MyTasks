namespace MicrosoftDocsParsing.Validators;

public interface IValidator
{
    public static abstract Task<string> ValidateAsync(string filePath);
}