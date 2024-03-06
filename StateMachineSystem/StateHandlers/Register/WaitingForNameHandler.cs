using System.Text;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForName, BotState.Register_WaitingForAge, MessageType.Text)]
public class WaitingForNameHandler : StateHandlerWithKeyboardMarkup
{
    private readonly ILogger<WaitingForNameHandler> _logger;
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForNameHandler(ILogger<WaitingForNameHandler> logger, IStringLocalizer<Program> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var expectedName = update.Message.From.FirstName;

        var keyboardButtons = new List<List<string>>();
        
        if (user.Name != null && user.Name != expectedName)
        {
            keyboardButtons.Add(new() { _localizer["LeaveCurrent"] + user.Name });
        }
        
        keyboardButtons.Add(new () { expectedName });

        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        // Можно вынести отправку text message.
        await botClient.SendTextMessageAsync(user.Id, _localizer["WaitingName"],
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userName = update.Message?.Text;

        if (userName.Contains(_localizer["LeaveCurrent"]))
        {
            ChangeState(user);

            return;
        }

        if (userName.Length > 40)
        {
            await botClient.SendTextMessageAsync(user.Id, _localizer["Error_LongName"],
                cancellationToken: cancellationToken);
            return;
        }
        
        user.Name = userName;
        
        _logger.LogInformation(userName);

        ChangeState(user);
    }
}