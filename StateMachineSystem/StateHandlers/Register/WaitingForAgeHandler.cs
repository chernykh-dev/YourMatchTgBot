using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForAge, BotState.Register_WaitingForGender, MessageType.Text)]
public class WaitingForAgeHandler : StateHandlerWithKeyboardMarkup
{
    private readonly ILogger<WaitingForAgeHandler> _logger;
    private readonly IStringLocalizer<Program> _localizer;
    private readonly IUserService _userService;

    public WaitingForAgeHandler(ILogger<WaitingForAgeHandler> logger,
        IStringLocalizer<Program> localizer,
        IUserService userService)
    {
        _logger = logger;
        _localizer = localizer;
        _userService = userService;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var chat = await botClient.GetChatAsync(user.Id, cancellationToken: cancellationToken);
        
        var userDescription = chat.Bio;

        var keyboardButtons = new List<List<string>>();
        
        var guessedUserAgeFound = TryGetGuessedUserAge(userDescription, out var guessedUserAge);
        
        if (user.Age != null)
        {
            if (!guessedUserAgeFound || user.Age.ToString() != guessedUserAge)
                keyboardButtons.Add(new() { _localizer["LeaveCurrent"] + user.Age });
        }

        if (guessedUserAgeFound)
            keyboardButtons.Add(new () { guessedUserAge });
        
        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(user.Id,
            string.Format(_localizer["WaitingAge"], GetLocalizedShortDatePattern()),
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput.Contains(_localizer["LeaveCurrent"]))
        {
            ChangeState(user);

            return;
        }

        uint userAge;
        /*
        if (DateTime.TryParseExact(userInput,
                CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, 
                null, 
                DateTimeStyles.None,
                out var userBirthDate))
        {
            userAge = (uint)(DateTime.Now.Year - userBirthDate.Year);
        }
        else */if (!uint.TryParse(userInput, out userAge))
        {
            return;
        }

        if (userAge > 125)
        {
            await botClient.SendTextMessageAsync(user.Id,
                _localizer["Error_IncorrectAge"],
                cancellationToken: cancellationToken);

            return;
        }

        if (userAge < 16)
        {
            await botClient.SendTextMessageAsync(user.Id,
                _localizer["Error_TooYoung"],
                cancellationToken: cancellationToken);

            user.State = BotState.Start;

            return;
        }

        user.Age = (short)userAge;
        _logger.LogInformation(userAge.ToString());

        ChangeState(user);
    }
    
    private static bool TryGetGuessedUserAge(string? userDescription, out string guessedUserAge)
    {
        guessedUserAge = "";
        
        if (userDescription == null || string.IsNullOrEmpty(userDescription))
            return false;
        
        var matches = Regex.Matches(userDescription, @"\d+");

        switch (matches.Count)
        {
            case <= 0:
                return false;
            case > 1:
                guessedUserAge = matches[0].Value;
                
                matches = Regex.Matches(userDescription, 
                    @"(\d+)\s*(?:[yY][\.\/]?[oOeE]|years old|-year-old|л\.|лет|year)");
                if (matches.Count > 0)
                {
                    guessedUserAge = matches[0].Groups[0].Value;
                    return true;
                }
                break;
            default:
                guessedUserAge = matches[0].Value;
                break;
        }

        return true;
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