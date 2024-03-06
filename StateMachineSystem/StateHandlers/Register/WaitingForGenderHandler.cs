using System.Globalization;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForGender, BotState.Register_WaitingForInterests, MessageType.Text)]
public class WaitingForGenderHandler : StateHandlerWithKeyboardMarkup
{
    private readonly ILogger<WaitingForGenderHandler> _logger;
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForGenderHandler(ILogger<WaitingForGenderHandler> logger, IStringLocalizer<Program> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var keyboardButtons = new List<List<string>> { new() { _localizer["Man"], _localizer["Women"] } };

        if (user.Gender != null)
        {
            keyboardButtons.Insert(0, new () { _localizer["LeaveCurrent"] + _localizer[user.Gender.ToString()] });
        }
        
        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(user.Id, _localizer["WaitingGender"], replyMarkup: replyKeyboardMarkup,
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

        if (userInput == _localizer["Man"])
        {
            user.Gender = Gender.Man;
        }
        else if (userInput == _localizer["Women"])
        {
            user.Gender = Gender.Women;
        }
        else
        {
            await botClient.SendTextMessageAsync(user.Id, _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);
            
            return;
        }

        ChangeState(user);
    }
}