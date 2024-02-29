using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.Models;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForPartnerGender)]
public class WaitingForPartnerGenderHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForPartnerGenderHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var replyKeyboardMarkup = GetReplyKeyboard(new[] { new string[] { _localizer["Man"], _localizer["Women"] } });

        await botClient.SendTextMessageAsync(update.Message.Chat.Id, _localizer["WaitingPartnerGender"], replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput == _localizer["Man"])
            user.PartnerGender = Gender.Man;
        else if (userInput == _localizer["Women"])
            user.PartnerGender = Gender.Women;
        else
            return;

        user.State = BotState.Register_WaitingForInterests;
    }
}