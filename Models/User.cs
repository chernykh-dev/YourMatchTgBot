using YourMatchTgBot.StateMachineSystem;

namespace YourMatchTgBot.Models;

public class User
{
    public long Id { get; set; }
    
    public BotState State { get; set; }

    public string Name { get; set; }

    public short Age { get; set; }
    
    public Gender Gender { get; set; }
    
    public Gender PartnerGender { get; set; }

    public string City { get; set; }

    public string Description { get; set; }

    public string ZodiacSign { get; set; }

    public int Height { get; set; }

    public string Education { get; set; }

    public List<Interest> Interests { get; set; } = new List<Interest>();

    public List<string> Photos { get; set; } = new List<string>();
    
    public string? LanguageCode { get; set; }
}

public enum Gender
{
    Man = 0,
    Women
}