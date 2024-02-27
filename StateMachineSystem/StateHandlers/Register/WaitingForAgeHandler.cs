using System.Globalization;
using System.Text;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForAge)]
public class WaitingForAgeHandler : IStateHandler
{
    private readonly ILogger<WaitingForAgeHandler> _logger;
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForAgeHandler(ILogger<WaitingForAgeHandler> logger, IStringLocalizer<Program> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(update.Message.Chat.Id,
            string.Format(_localizer["WaitingAge"], GetLocalizedShortDatePattern()),
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }

    public async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        uint userAge;
        if (DateTime.TryParseExact(userInput,
                CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, 
                null, 
                DateTimeStyles.None,
                out var userBirthDate))
        {
            userAge = (uint)(DateTime.Now.Year - userBirthDate.Year);
        }
        else if (!uint.TryParse(userInput, out userAge) || !IsValidAge(userAge))
        {
            return;
        }
        
        _logger.LogInformation(userAge.ToString());

        user.State = BotState.Register_WaitingForGender;
    }

    private static bool IsValidAge(uint userAge)
    {
        return userAge <= 125;
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