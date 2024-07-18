using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class MatchesService
{
    private readonly ApplicationDbContext _context;

    public MatchesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async void Add(Match match)
    {
        _context.Matches.Add(match);

        await _context.SaveChangesAsync();
    }

    public Match? GetMatchForUsersOrNull(User fromUser, User toUser)
    {
        return _context.Matches
            .FirstOrDefault(m => m.MatchFromUser == fromUser && m.MatchToUser == toUser && !m.Handled);
    }

    public List<Match> MatchedForUser(User user)
    {
        return _context.Matches
            .Where(m => m.MatchToUser == user && !m.Handled)
            .OrderBy(m => m.Time)
            .ToList();
    }

    public List<Match> AllMatchedForUser(User user)
    {
        return _context.Matches
            .Where(m => m.MatchToUser == user)
            .ToList();
    }

    public async void Remove(Match match)
    {
        _context.Matches.Remove(match);

        await _context.SaveChangesAsync();
    }
}