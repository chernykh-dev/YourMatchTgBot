using YourMatchTgBot.StateMachineSystem;

namespace YourMatchTgBot.Models;

public class User
{
    public long Id { get; set; }
    
    public BotState State { get; set; }

    public string? LanguageCode { get; set; }
    
    public string? Name { get; set; }

    public short? Age { get; set; }
    
    public Gender? Gender { get; set; }
    
    public int InterestsFlags { get; set; }
    
    public int TemporaryInterestsFlags { get; set; }
    
    public virtual List<TempInterest> TemporaryInterests { get; set; } = new();

    public virtual List<Interest> Interests { get; set; } = new ();

    public int? Height { get; set; }

    public virtual City? City { get; set; }
    
    public virtual List<UserMedia> Photos { get; set; } = new ();
    
    public virtual List<TempUserMedia> TemporaryPhotos { get; set; } = new ();

    public string? Description { get; set; }
    
    public Gender? PartnerGender { get; set; }

    public string? ZodiacSign { get; set; }

    public string? Education { get; set; }
}