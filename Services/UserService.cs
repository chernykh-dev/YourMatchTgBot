using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User? GetUserById(long id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public User AddUser(User user)
    {
        var userEntry = _context.Users.Add(user);

        _context.SaveChanges();

        return userEntry.Entity;
    }
}