using System.Globalization;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForGender)]
public class WaitingForGender : StateHandlerWithKeyboardMarkup
{
    private readonly ILogger<WaitingForGender> _logger;
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForGender(ILogger<WaitingForGender> logger, IStringLocalizer<Program> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var replyKeyboardMarkup = GetReplyKeyboard(new[] { new string[] { _localizer["Man"], _localizer["Women"] } });

        await botClient.SendTextMessageAsync(update.Message.Chat.Id, _localizer["WaitingGender"], replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput != _localizer["Man"] && userInput != _localizer["Women"])
        {
            return;
        }
        
        // Interests.

        user.State = BotState.Register_WaitingForInterests;
    }
}