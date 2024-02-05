using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class InterestService
{
    private List<Interest> _interests = new List<Interest>()
    {
        new Interest { Id = 1, Name = "книги" },
        new Interest { Id = 2, Name = "игры" },
        new Interest { Id = 3, Name = "прогулки" },
        new Interest { Id = 4, Name = "клубы" },
        new Interest { Id = 5, Name = "фильмы" },
        new Interest { Id = 6, Name = "фитнес" },
        new Interest { Id = 7, Name = "it" },
    };

    public Interest? GetInterestById(long id)
    {
        return _interests.FirstOrDefault(i => i.Id == id);
    }

    public List<Interest> GetInterestsByIds(params long[] ids)
    {
        return _interests.Where(i => ids.Contains(i.Id)).ToList();
    }
}