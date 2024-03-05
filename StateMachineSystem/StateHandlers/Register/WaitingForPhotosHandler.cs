using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Models;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForPhotos)]
public class WaitingForPhotosHandler : StateHandlerWithKeyboardMarkup
{
    private const int MAX_PHOTOS_COUNT = 6;
    
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForPhotosHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var keyboardButtons = new List<List<string>>();
        
        if (user.TemporaryPhotos.Count == 0)
        {
            if (user.Photos.Count > 0)
            {
                keyboardButtons.Add(new () { _localizer["LeaveCurrentPhotos"] });
            }
            
            var photos =
                await botClient.GetUserProfilePhotosAsync(update.Message.Chat.Id, 0, MAX_PHOTOS_COUNT,
                    cancellationToken: cancellationToken);
            
            if (photos.TotalCount > 0)
            {
                keyboardButtons.Add(new() { _localizer["GetPhotosFromProfile"] });
            }
        }
        else
        {
            keyboardButtons.Add(new () { _localizer["Continue"] });
        }

        // TODO: Менять текст в зависимости загружал ли пользователь фото до этого.
        await botClient.SendTextMessageAsync(update.Message.Chat,
            string.Format(_localizer["WaitingPhotos"], MAX_PHOTOS_COUNT, user.TemporaryPhotos.Count),
            replyMarkup: GetReplyKeyboard(keyboardButtons), cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (update.Message.Text == _localizer["LeaveCurrentPhotos"])
        {
            user.State = BotState.Register_WaitingForDescription;

            return;
        }
        
        user.Photos.Clear();
        
        if (update.Message.Text == _localizer["GetPhotosFromProfile"])
        {
            var photos =
                await botClient.GetUserProfilePhotosAsync(update.Message.Chat.Id, 0, MAX_PHOTOS_COUNT,
                    cancellationToken: cancellationToken);

            if (photos.TotalCount == 0)
                return;

            // TODO: Возможно пользователь захочет добавить еще.
            foreach (var photo in photos.Photos)
            {
                user.Photos.Add(new UserMedia { UserId = user.Id, MediaFileId = photo.Last().FileId });
            }

            user.State = BotState.Register_WaitingForDescription;
            return;
        }

        if (update.Message.Text == _localizer["Continue"])
        {
            user.Photos.AddRange(user.TemporaryPhotos.Select(u => new UserMedia
                { UserId = u.UserId, MediaFileId = u.MediaFileId, MediaType = u.MediaType}));
            user.TemporaryPhotos.Clear();
            
            user.State = BotState.Register_WaitingForDescription;
            return;
        }

        if (!update.Message.Type.Is(MessageType.Photo, MessageType.Video))
            return;

        var mediaFileId = "";
        switch (update.Message.Type)
        {
            case MessageType.Photo:
                mediaFileId = update.Message.Photo.Last().FileId;
                break;

            case MessageType.Video:
                mediaFileId = update.Message.Video.FileId;
                break;

            default:
                break;
        }

        user.TemporaryPhotos.Add(new TempUserMedia
            { UserId = user.Id, MediaFileId = mediaFileId, MediaType = update.Message.Type });

        if (user.TemporaryPhotos.Count == MAX_PHOTOS_COUNT)
        {
            user.Photos.AddRange(user.TemporaryPhotos.Select(u => new UserMedia
                { UserId = u.UserId, MediaFileId = u.MediaFileId, MediaType = u.MediaType}));
            user.TemporaryPhotos.Clear();

            user.State = BotState.Register_WaitingForDescription;
        }
    }
}

public static class EnumExt
{
    public static bool Is<T>(this T selfEnum, params T[] values)
        where T : Enum
    {
        return values.Contains(selfEnum);
    }
}