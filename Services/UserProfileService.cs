using Microsoft.Extensions.Localization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.Services;

public class UserProfileService
{
    private IUserService _userService;
    private IStringLocalizer<Program> _localizer;

    public UserProfileService(IUserService userService, IStringLocalizer<Program> localizer)
    {
        _userService = userService;
        _localizer = localizer;
    }

    public IEnumerable<IAlbumInputMedia> GetUserProfileMessage(User user)
    {
        var photos = user.Photos;

        var album = photos
            .Select<UserMedia, IAlbumInputMedia>(userPhoto =>
            {
                var inputFile = InputFile.FromFileId(userPhoto.MediaFileId);
                
                switch (userPhoto.MediaType)
                {
                    case MessageType.Photo:
                        return new InputMediaPhoto(inputFile);
                    case MessageType.Video:
                        return new InputMediaVideo(inputFile);
                    default:
                        break;
                }
                
                return new InputMediaPhoto(InputFile.FromFileId(userPhoto.MediaFileId));
            })
            .ToList();
        
        ((InputMedia)album.First()).Caption = user.GetTextProfile(_localizer);

        return album;
    }
}