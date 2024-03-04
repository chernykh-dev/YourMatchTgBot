using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace YourMatchTgBot.Models;

public class Interest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; } = new();
}

[PrimaryKey("UserId", "InterestId")]
public class TempInterest
{
    public long UserId { get; set; }
    
    public int InterestId { get; set; }
    
    public virtual User User { get; set; }
    public virtual Interest Interest { get; set; }
}