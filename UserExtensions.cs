using Microsoft.Extensions.Localization;
using YourMatchTgBot.Models;

namespace YourMatchTgBot;

public static class UserExtensions
{
    public static string GetTextProfile(this User user, IStringLocalizer<Program> localizer)
    {
        // var informationText = $"\u2649{user.ZodiacSign} \ud83d\udccf{user.Height} \ud83d\udcda{user.Education}";
        var commonInfoText = $"\ud83d\udccf{user.Height}";

        var interestsText = user.Interests
            .Aggregate("", (current, interest) => current + $"{interest.Name}{localizer[interest.Name]}  ");

        return $"{user.Name}, {user.Age}, {user.City.Name}\n\n{commonInfoText}  {interestsText}\n\n{user.Description}";
    }
}