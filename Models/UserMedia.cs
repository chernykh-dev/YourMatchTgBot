using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.Enums;

namespace YourMatchTgBot.Models;

[PrimaryKey("UserId", "MediaFileId")]
public class UserMedia
{
    public long UserId { get; set; }
    
    public string MediaFileId { get; set; }
    
    public MessageType MediaType { get; set; }
}