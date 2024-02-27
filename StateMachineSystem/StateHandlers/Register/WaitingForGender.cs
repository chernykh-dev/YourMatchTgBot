using System.Globalization;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForGender)]
public class WaitingForGender : IStateHandler
{
    private readonly ILogger<WaitingForGender> _logger;
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForGender(ILogger<WaitingForGender> logger, IStringLocalizer<Program> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var reply = new ReplyKeyboardMarkup(new[] { new KeyboardButton("Man"), new KeyboardButton("Women") });
        reply.ResizeKeyboard = true;
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Gender:", replyMarkup: reply,
            cancellationToken: cancellationToken);
    }

    public async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput is not "Man" and "Women")
        {
            return;
        }
        
        // Interests.

        user.State = BotState.Register_WaitingForInterests;
    }
}