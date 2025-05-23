using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForHeight, BotState.Register_WaitingForLocation, MessageType.Text)]
public class WaitingForHeightHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForHeightHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var replyKeyboardTexts = new List<List<string>>();

        if (user.Height != null)
        {
            replyKeyboardTexts.Add(new () { _localizer["LeaveCurrent"] + user.Height.ToString() });
            
            // replyKeyboardTexts.Add(new () { "​" });
        }
        
        for (var i = 140; i < 211; i++)
        {
            var row = new List<string>
            {
                new(i.ToString())
            };

            replyKeyboardTexts.Add(row);
        }

        var replyKeyboardMarkup = GetReplyKeyboard(replyKeyboardTexts);

        await botClient.SendTextMessageAsync(user.Id, _localizer["WaitingHeight"],
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userHeightString = update.Message.Text;

        if (userHeightString.Contains(_localizer["LeaveCurrent"]))
        {
            ChangeState(user);

            return;
        }

        if (!int.TryParse(userHeightString, out var userHeight))
        {
            await botClient.SendTextMessageAsync(user.Id, _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);

            return;
        }
        
        user.Height = userHeight;

        ChangeState(user);
    }
}