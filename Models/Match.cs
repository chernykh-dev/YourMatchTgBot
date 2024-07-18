using Microsoft.EntityFrameworkCore;

namespace YourMatchTgBot.Models;

[PrimaryKey("MatchFromUserId", "MatchToUserId", "Time")]
public class Match
{
    public long MatchFromUserId { get; set; }

    public long MatchToUserId { get; set; }

    public DateTime Time { get; set; }

    public bool Handled { get; set; }
    

    public virtual User MatchFromUser { get; set; }

    public virtual User MatchToUser { get; set; }

    public int FromUserMessageId { get; set; }
}