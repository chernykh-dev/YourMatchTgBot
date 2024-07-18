using System.Diagnostics;
using Geolocation;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.WatchProfiles, BotState.WatchProfiles, MessageType.Text /* TODO: Возможно + стикеры */)]
public class WatchProfilesHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IUserService _userService;
    private readonly UserProfileService _userProfileService;
    private readonly UserMatchingService _userMatchingService;
    private readonly MatchesService _matchesService;
    private readonly ILogger<WatchProfilesHandler> _logger;
    private readonly IStringLocalizer<Program> _localizer;
    private readonly ApplicationDbContext _context;

    public WatchProfilesHandler(IUserService userService, UserProfileService userProfileService, UserMatchingService userMatchingService, ILogger<WatchProfilesHandler> logger, IStringLocalizer<Program> localizer, MatchesService matchesService, ApplicationDbContext context)
    {
        _userService = userService;
        _userProfileService = userProfileService;
        _userMatchingService = userMatchingService;
        _logger = logger;
        _localizer = localizer;
        _matchesService = matchesService;
        _context = context;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var matches = _matchesService.MatchedForUser(user);

        if (matches.Count > 0)
        {
            var replyKeyboardMarkup = GetLikesReplyKeyboard();

            await botClient.SendTextMessageAsync(user.Id,
                _localizer["WaitingAnswerForMatch"],
                replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);

            var matchedAlbum =
                await _userProfileService.GetUserProfileMessage(matches[0].MatchFromUser, cancellationToken,
                    user.LanguageCode);

            await botClient.SendMediaGroupAsync(user.Id, matchedAlbum, cancellationToken: cancellationToken);

            // user.CurrentUserForMatch = matches[0].MatchFromUser;
            user.CurrentUserForMatch = null;
            user.CurrentUserForMatchMessageId = null;

            return;
        }

        List<User> foundUsers;
        User foundUser;
        do
        {
            foundUsers = _userMatchingService.MatchForUserByDistance(user);

            if (foundUsers.Count == 0)
            {
                await botClient.SendTextMessageAsync(user.Id,
                    "Users not found", cancellationToken: cancellationToken);

                user.State = BotState.Register_ShowProfile;

                return;
            }

            do
            {
                foundUser = foundUsers[user.CurrentOffset];

                var userMatches = _matchesService.AllMatchedForUser(user);
                var foundMatch = userMatches.Find(m => m.MatchFromUser == foundUser);
                if (foundMatch != null)
                {
                    user.CurrentOffset++;
                    userMatches.Remove(foundMatch);
                }
                else
                {
                    break;
                }
            }
            while (user.CurrentOffset < foundUsers.Count);
        }
        while (user.CurrentOffset == foundUsers.Count);

        user.CurrentUserForMatch = foundUser;
        user.CurrentFoundUsersCount = foundUsers.Count;

        await _context.SaveChangesAsync(cancellationToken: cancellationToken);

        var album = await _userProfileService.GetUserProfileMessage(foundUser, cancellationToken, user.LanguageCode);

        var messages = await botClient.SendMediaGroupAsync(user.Id, album, cancellationToken: cancellationToken);

        user.CurrentUserForMatchMessageId = messages.First().MessageId;
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (update.Message.Text == PROFILE_SYMBOL)
        {
            user.State = BotState.Register_ShowProfile;

            return;
        }

        if (update.Message.Text != LIKE_SYMBOL && update.Message.Text != DISLIKE_SYMBOL)
        {
            return;
        }

        if (user.CurrentUserForMatch != null)
        {
            var currentUserForMatchId = user.CurrentUserForMatch.Id;

            var matchToUser = _userService.GetUserById(currentUserForMatchId);

            var existMatch = _matchesService.GetMatchForUsersOrNull(matchToUser, user);

            if (existMatch != null)
            {
                await HandleMatch(existMatch, botClient, update, cancellationToken);
            }
            else if (update.Message.Text == LIKE_SYMBOL && currentUserForMatchId >= 0)
            {
                Debug.Assert(user.CurrentUserForMatchMessageId != null, "user.CurrentUserForMatchMessageId != null");

                _matchesService.Add(new Match
                {
                    MatchFromUser = user,
                    MatchToUser = matchToUser,
                    Time = DateTime.Now,
                    FromUserMessageId = user.CurrentUserForMatchMessageId.Value
                });

                user.CurrentUserForMatch = null;

                if (matchToUser.State == BotState.WatchProfiles && matchToUser.CurrentUserForMatch != user)
                    await botClient.SendTextMessageAsync(currentUserForMatchId, _localizer["MatchFound"], cancellationToken: cancellationToken);
            }

            // Обрабатываем последнего пользователя в серии.
            user.CurrentOffset++;
            if (user.CurrentOffset == user.CurrentFoundUsersCount)
            {
                user.CurrentOffset = 0;

                // Последняя серия поиска.
                if (user.CurrentFoundUsersCount < UserMatchingService.USERS_BY_SEARCH)
                {
                    user.SearchOffset = 0;
                }
                // Не последняя серия поиска.
                else
                {
                    user.SearchOffset += UserMatchingService.USERS_BY_SEARCH;
                }
            }

            return;
        }
        /*else if (update.Message.Text == DISLIKE_SYMBOL)
        {
            // Обрабатываем последнего пользователя в серии.
            user.CurrentOffset++;
            if (user.CurrentOffset == user.CurrentFoundUsersCount)
            {
                user.CurrentOffset = 0;

                // Последняя серия поиска.
                if (user.CurrentFoundUsersCount < UserMatchingService.USERS_BY_SEARCH)
                {
                    user.SearchOffset = 0;
                }
                // Не последняя серия поиска.
                else
                {
                    user.SearchOffset += UserMatchingService.USERS_BY_SEARCH;
                }
            }

            return;
        }*/

        var matches = _matchesService.MatchedForUser(user);
        if (matches.Count > 0)
        {
            await HandleMatch(matches[0], botClient, update, cancellationToken);

            return;
        }
    }

    private async Task HandleMatch(Match match, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message.Text == LIKE_SYMBOL)
        {
            const string formatString = "[{0}](https://t.me/{1})"; // "[{0}](tg://user?id={1})";

            var matchFrom = await botClient.GetChatAsync(match.MatchFromUser.Id, cancellationToken: cancellationToken);
            var matchTo = await botClient.GetChatAsync(match.MatchToUser.Id, cancellationToken: cancellationToken);

            await botClient.SendTextMessageAsync(match.MatchFromUser.Id,
                string.Format(_localizer["GoToChat"], string.Format(formatString, match.MatchToUser.Name, matchTo.Username)),
                parseMode: ParseMode.MarkdownV2, cancellationToken: cancellationToken, replyToMessageId: match.FromUserMessageId);

            await botClient.SendTextMessageAsync(match.MatchToUser.Id,
                string.Format(_localizer["GoToChat"], string.Format(formatString, match.MatchFromUser.Name, matchFrom.Username)),
                parseMode: ParseMode.MarkdownV2, cancellationToken: cancellationToken);
        }

        match.Handled = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}