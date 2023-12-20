using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MicrosoftDocsParsing.Services.FileProcessor.Extensions;

public static class ParagraphExtension
{
    /// <summary>
    /// Паттерн для удаления eq из текста
    /// </summary>
    private const string NeedToRemovePattern = "eq(|\\n| ) ?";
    
    /// <summary>
    /// Удаление скрытых символов из параграфа
    /// </summary>
    /// <param name="paragraph">Параграф</param>
    /// <returns>Параграф без скрытых символов и "eq"</returns>
    public static string TextWithoutHiddenElements(this Paragraph paragraph)
    {
        var elementsWithoutHidden= paragraph.Descendants<Run>().
            Where(run => run.RunProperties?.Vanish is null).
            Select(run => run.InnerText);
        
        var text = string.Join("", elementsWithoutHidden);
        return Regex.Replace(text, NeedToRemovePattern, "");
    }
}