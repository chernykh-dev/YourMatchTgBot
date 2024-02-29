using Microsoft.Extensions.Localization;
using Telegram.Bot.Types;
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
            .Select(userPhoto => new InputMediaPhoto(InputFile.FromFileId(userPhoto.PhotoFileId)))
            .Cast<IAlbumInputMedia>()
            .ToList();

        ((InputMedia)album.First()).Caption = user.GetTextProfile(_localizer);

        return album;
    }
}