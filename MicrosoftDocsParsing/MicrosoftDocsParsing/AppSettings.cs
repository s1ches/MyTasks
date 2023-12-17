using System.Text.Json;

namespace MicrosoftDocsParsing;

public class AppSettings
{
    public string? FileDirectory { get; set; }
    
    public string? ParsedFiles { get; set; }
    
    public string? UnparsedFiles { get; set; }
    
    private static readonly Lazy<AppSettings> Lazy = new(() =>
    {
        var appsettingsPath = @"appsettings.json";
        
        if (!File.Exists(appsettingsPath))
            throw new Exception($"{appsettingsPath} not found");

        using var jsonConfig = File.OpenRead(appsettingsPath);
        
        return JsonSerializer.Deserialize<AppSettings>(jsonConfig)!;
    });

    public static AppSettings Instance => Lazy.Value;
}