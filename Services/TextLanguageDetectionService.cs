using NTextCat;

namespace YourMatchTgBot.Services;

public class TextLanguageDetectionService
{
    private const string PROFILE_FILE_PATH = "Resources/Core14.profile.xml";

    private static readonly RankedLanguageIdentifierFactory _factory = new ();
    private static readonly RankedLanguageIdentifier _identifier;

    static TextLanguageDetectionService()
    {
        _identifier = _factory.Load(PROFILE_FILE_PATH);
    }

    public static string DetectLanguage(string text)
    {
        var languages = _identifier.Identify(text);

        var mostCertainLanguage = languages.FirstOrDefault();

        return mostCertainLanguage != null ? mostCertainLanguage.Item1.Iso639_2T : "en";
    }
}