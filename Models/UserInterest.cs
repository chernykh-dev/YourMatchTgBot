namespace YourMatchTgBot.Models;

public class UserInterest
{
    public long UserId { get; set; }
    
    public User User { get; set; }
    
    public int InterestId { get; set; }
    
    public Interest Interest { get; set; }
}