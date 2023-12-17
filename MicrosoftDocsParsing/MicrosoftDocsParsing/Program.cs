using MicrosoftDocsParsing;
using MicrosoftDocsParsing.Services.FileProcessor;

class Program
{
    static async Task Main()
    {
        var parseResults = await FileProcessor.ProcesAllUnparsedFilesAsync();

        foreach(var result in parseResults)
            Console.WriteLine(result);
    }
}