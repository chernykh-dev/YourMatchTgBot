using Microsoft.Extensions.Localization;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;

namespace YourMatchTgBot;

public static class UserExtensions
{
    public static async Task<string> GetTextProfile(this User user, IInterestService interestService, IStringLocalizer<Program> localizer, CancellationToken cancellationToken, string languageCode)
    {
        var localizedCityName = user.City.Name;
        if (languageCode == "ru")
        {
            localizedCityName = await TranslateService.TranslateText(localizedCityName, "en", "ru", cancellationToken);
        }
        
        // var informationText = $"\u2649{user.ZodiacSign} \ud83d\udccf{user.Height} \ud83d\udcda{user.Education}";
        var commonInfoText = $"\ud83d\udccf{user.Height}";

        var interests =
            interestService.GetInterestsByIds(IInterestService.GetInterestsFlags(user.InterestsFlags).ToArray());
        
        var interestsText = interests
            .Aggregate("", (current, interest) => current + $"{interest.Name}{localizer[interest.Name]}  ");

        return $"{user.Name}, {user.Age}, {localizedCityName}\n\n{commonInfoText}  {interestsText}\n\n{user.Description}";
    }
}