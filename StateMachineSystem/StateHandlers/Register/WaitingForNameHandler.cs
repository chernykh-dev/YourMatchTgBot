using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForName)]
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

        var replyKeyboardMarkup = GetReplyKeyboardWithCancel(new[] { new[] { expectedName } }, _localizer);

        // Можно вынести отправку text message.
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, _localizer["WaitingName"],
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userName = update.Message.Text;
        
        _logger.LogInformation(userName);

        user.State = BotState.Register_WaitingForAge;
    }
}