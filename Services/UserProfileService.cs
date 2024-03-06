using System.Text;
using Microsoft.Extensions.Localization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.Services;

public class UserProfileService
{
    private static readonly char[] TO_REPLACE_CHARS = new char[]
        { /*'_', '*', '[', ']', '(', ')', '~', '`', '>', '#', '+', '-', '=', '|', '{', '}', '.', '!'*/ };
    
    private IUserService _userService;
    private IInterestService _interestService;
    private IStringLocalizer<Program> _localizer;

    public UserProfileService(IUserService userService, IStringLocalizer<Program> localizer, IInterestService interestService)
    {
        _userService = userService;
        _localizer = localizer;
        _interestService = interestService;
    }

    public async Task<IEnumerable<IAlbumInputMedia>> GetUserProfileMessage(User user, CancellationToken cancellationToken, string languageCode)
    {
        var photos = user.Photos;

        var album = photos
            .Select<UserMedia, IAlbumInputMedia>(userPhoto =>
            {
                InputFile inputFile;
                if (userPhoto.MediaFileId.Contains("://"))
                    inputFile = InputFile.FromUri(userPhoto.MediaFileId);
                else
                    inputFile = InputFile.FromFileId(userPhoto.MediaFileId);
                
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

        var albumFirst = (InputMedia)album.First();

        var albumCaption = new StringBuilder(await user.GetTextProfile(_interestService, _localizer, cancellationToken, languageCode));

        foreach (var replaceChar in TO_REPLACE_CHARS)
        {
            albumCaption.Replace(replaceChar.ToString(), $"\\{replaceChar}");
        }

        albumFirst.Caption = albumCaption.ToString();
        // albumFirst.ParseMode = ParseMode.MarkdownV2;

        return album;
    }
}