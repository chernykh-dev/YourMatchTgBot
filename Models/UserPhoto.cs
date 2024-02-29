using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace YourMatchTgBot.Models;

[PrimaryKey("UserId", "PhotoFileId")]
public class UserPhoto
{
    public long UserId { get; set; }
    
    public string PhotoFileId { get; set; }
}