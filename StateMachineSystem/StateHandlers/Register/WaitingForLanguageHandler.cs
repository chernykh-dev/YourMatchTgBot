using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForLanguage, BotState.Register_ShowTermsOfUse, MessageType.Text)]
public class WaitingForLanguageHandler : StateHandlerWithKeyboardMarkup
{
    private const string ENGLISH_ANSWER = "English";
    private const string RUSSIAN_ANSWER = "Русский";

    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForLanguageHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var keyboardButtons = new List<List<string>> { new() { ENGLISH_ANSWER, RUSSIAN_ANSWER } };

        if (user.LanguageCode != null)
        {
            keyboardButtons.Insert(0, new () { _localizer["LeaveCurrent"] + _localizer[user.LanguageCode] });
        }
        
        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(user.Id, _localizer["WaitingLanguage"], replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput.Contains(_localizer["LeaveCurrent"]))
        {
            ChangeState(user);

            return;
        }

        if (userInput == ENGLISH_ANSWER)
        {
            user.LanguageCode = "en";
        }
        else if (userInput == RUSSIAN_ANSWER)
        {
            user.LanguageCode = "ru";
        }
        else
        {
            await botClient.SendTextMessageAsync(user.Id, _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);
            
            return;
        }

        Program.ChangeCultureInfo(user.LanguageCode);
        ChangeState(user);
    }
}