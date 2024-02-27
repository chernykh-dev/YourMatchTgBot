using YourMatchTgBot.StateMachineSystem;

namespace YourMatchTgBot.Models;

public class User
{
    public long Id { get; set; }
    
    public BotState State { get; set; }

    public string Name { get; set; }

    public short Age { get; set; }

    public string City { get; set; }

    public string Description { get; set; }

    public string ZodiacSign { get; set; }

    public int Height { get; set; }

    public string Education { get; set; }

    public List<Interest> Interests { get; set; }



    public string GetTextProfile()
    {
        var informationText = $"\u2649{ZodiacSign} \ud83d\udccf{Height} \ud83d\udcda{Education}";

        var interestsText = Interests.Aggregate("", (current, interest) => current + $"#{interest.Name} ");

        return $"{Name}, {Age}, {City}\n\n{Description}\n\n{informationText}\n\n{interestsText}";
    }
}