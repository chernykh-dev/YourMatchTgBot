using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForInterests)]
public class WaitingForInterestsHandler : StateHandlerWithKeyboardMarkup
{
    private const int MAX_INTERESTS_COUNT = 3;
    
    private readonly InterestService _interestService;
    private IStringLocalizer<Program> _localizer;

    public WaitingForInterestsHandler(InterestService interestService, IStringLocalizer<Program> localizer)
    {
        _interestService = interestService;
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        using var interestsEnumerator = _interestService.GetInterests().AsEnumerable().GetEnumerator();
        interestsEnumerator.MoveNext();
        
        var replyKeyboardTexts = new List<List<string>>();
        for (var i = 0; i < 5; i++)
        {
            replyKeyboardTexts.Add(new List<string>());
            for (var j = 0; j < 3 - i % 2; j++)
            {
                var interestName = interestsEnumerator.Current.Name;

                var localizedInterestName = $"{interestName} {_localizer[interestName]}";

                if (user.Interests.Any(i => i.Name == interestName))
                    localizedInterestName = "​";

                replyKeyboardTexts[i].Add(localizedInterestName);
                interestsEnumerator.MoveNext();
            }
        }

        var replyKeyboardMarkup = GetReplyKeyboard(replyKeyboardTexts);

        await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
            string.Format(_localizer["WaitingInterests"], user.Interests.Count, MAX_INTERESTS_COUNT),
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userInterest = update.Message.Text[..2];

        var interest = _interestService.GetInterestByName(userInterest);

        if (interest == null)
            return;
        
        user.Interests.Add(interest);

        if (user.Interests.Count == MAX_INTERESTS_COUNT)
            user.State = BotState.Register_WaitingForHeight;
    }
}