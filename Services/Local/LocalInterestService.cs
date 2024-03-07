using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services.Local;

public class LocalInterestService : IInterestService
{
    private List<Interest> _interests = new List<Interest>()
    {
        new Interest { Id = 1, Name = "📚" },
        new Interest { Id = 2, Name = "🎲" },
        new Interest { Id = 3, Name = "🚶" },
        new Interest { Id = 4, Name = "💃" },
        new Interest { Id = 5, Name = "🎞" },
        new Interest { Id = 6, Name = "🏅" },
        new Interest { Id = 7, Name = "💻" },
        new Interest { Id = 8, Name = "🚙" },
        new Interest { Id = 8, Name = "🏔" },
        new Interest { Id = 8, Name = "🍲" },
        new Interest { Id = 8, Name = "🎧" },
        new Interest { Id = 8, Name = "🍳" },
        new Interest { Id = 8, Name = "🛍" },
    };

    public Interest? GetInterestById(long id)
    {
        return _interests.FirstOrDefault(i => i.Id == id);
    }

    public List<Interest> GetInterestsByIds(params long[] ids)
    {
        return _interests.Where(i => ids.Contains(i.Id)).ToList();
    }

    public List<Interest> GetInterests()
    {
        return _interests;
    }

    public Interest? GetInterestByName(string name)
    {
        return _interests.FirstOrDefault(i => i.Name == name);
    }
}