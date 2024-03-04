using System.Globalization;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForGender)]
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
            keyboardButtons.Add(new () { _localizer[user.Gender.ToString()] });
        }
        
        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["WaitingGender"], replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

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
            await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);
            
            return;
        }

        user.State = BotState.Register_WaitingForPartnerGender;
    }
}