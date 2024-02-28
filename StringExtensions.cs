using System.Text;

namespace YourMatchTgBot;

public static class StringExtensions
{
    public static string StrikeThrough(this string value)
    {
        var sb = new StringBuilder();
        foreach (var character in value)
            sb.Append(character + "\u0336");

        return sb.ToString();
    }
}