using Microsoft.Extensions.Localization;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;

namespace YourMatchTgBot;

public static class UserExtensions
{
    public static async Task<string> GetTextProfile(this User user, IStringLocalizer<Program> localizer, CancellationToken cancellationToken)
    {
        var localizedCityName = user.City.Name;
        if (user.LanguageCode == "ru")
        {
            localizedCityName = await TranslateService.TranslateText(localizedCityName, "en", "ru", cancellationToken);
        }
        
        // var informationText = $"\u2649{user.ZodiacSign} \ud83d\udccf{user.Height} \ud83d\udcda{user.Education}";
        var commonInfoText = $"\ud83d\udccf{user.Height}";

        var interestsText = user.Interests
            .Aggregate("", (current, interest) => current + $"{interest.Name}{localizer[interest.Name]}  ");

        return $"{user.Name}, {user.Age}, {localizedCityName}\n\n{commonInfoText}  {interestsText}\n\n{user.Description}";
    }
}