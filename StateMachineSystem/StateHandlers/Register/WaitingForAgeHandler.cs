using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForAge)]
public class WaitingForAgeHandler : StateHandlerWithKeyboardMarkup
{
    private readonly ILogger<WaitingForAgeHandler> _logger;
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForAgeHandler(ILogger<WaitingForAgeHandler> logger, IStringLocalizer<Program> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var chat = await botClient.GetChatAsync(update.Message.Chat, cancellationToken: cancellationToken);
        
        var userDescription = chat.Bio;

        var replyKeyboardMarkup = GetGuessedUserAgeReplyKeyboardMarkup(userDescription);

        await botClient.SendTextMessageAsync(update.Message.Chat.Id,
            string.Format(_localizer["WaitingAge"], GetLocalizedShortDatePattern()),
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        uint userAge;
        if (DateTime.TryParseExact(userInput,
                CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, 
                null, 
                DateTimeStyles.None,
                out var userBirthDate))
        {
            userAge = (uint)(DateTime.Now.Year - userBirthDate.Year);
        }
        else if (!uint.TryParse(userInput, out userAge) || !IsValidAge(userAge))
        {
            return;
        }

        user.Age = (short)userAge;
        _logger.LogInformation(userAge.ToString());

        user.State = BotState.Register_WaitingForGender;
    }
    
    private static IReplyMarkup GetGuessedUserAgeReplyKeyboardMarkup(string? userDescription)
    {
        var replyKeyboardRemove = new ReplyKeyboardRemove();
        if (userDescription == null || string.IsNullOrEmpty(userDescription))
            return replyKeyboardRemove;
        
        string? guessedUserAge = null;
        var matches = Regex.Matches(userDescription, @"\d+");

        switch (matches.Count)
        {
            case <= 0:
                return replyKeyboardRemove;
            case > 1:
                guessedUserAge = matches[0].Value;
                
                matches = Regex.Matches(userDescription, 
                    @"(\d+)\s*(?:[yY][\.\/]?[oOeE]|years old|-year-old|л\.|лет|year)");
                if (matches.Count > 0)
                    return GetReplyKeyboard(new[] { new[] { matches[0].Groups[0].Value } });
                break;
            default:
                guessedUserAge = matches[0].Value;
                break;
        }
        
        return GetReplyKeyboard(new[] { new[] { guessedUserAge } });
    }

    private static bool IsValidAge(uint userAge)
    {
        return userAge <= 125;
    }

    private string GetLocalizedShortDatePattern()
    {
        var stringBuilder = new StringBuilder(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        return stringBuilder
            .Replace("dd", _localizer["dd"])
            .Replace("MM", _localizer["MM"])
            .Replace("yyyy", _localizer["yyyy"])
            .ToString();
    }
}