using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class InterestService : IInterestService
{
    private readonly ApplicationDbContext _context;

    public InterestService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Interest? GetInterestById(long id)
    {
        return _context.Interests.Find(id);
    }

    public List<Interest> GetInterestsByIds(params long[] ids)
    {
        return _context.Interests.Where(i => ids.Any(id => id == i.Id)).ToList();
    }

    public List<Interest> GetInterests()
    {
        return _context.Interests.ToList();
    }

    public Interest? GetInterestByName(string name)
    {
        return _context.Interests.FirstOrDefault(i => i.Name == name);
    }
}