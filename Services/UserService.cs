using Microsoft.EntityFrameworkCore;
using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly Random Random = new Random();

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User? GetUserById(long id)
    {
        return _context.Users
            .Include(u => u.Photos)
            .Include(u => u.City)
            .Include(u => u.TemporaryPhotos)
            .FirstOrDefault(u => u.Id == id);
    }

    public User? FindUserForUser(User user)
    {
        var skip = (int)(Random.NextDouble() * _context.Users.Count());

        return _context.Users
            .OrderBy(u => u.Id)
            .Skip(skip)
            .Take(1)
            .Include(u => u.Photos)
            .Include(u => u.City)
            .Include(u => u.TemporaryPhotos)
            .First();
    }

    public User AddUser(User user)
    {
        var userEntry = _context.Users.Add(user);

        _context.SaveChanges();

        return userEntry.Entity;
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);

        _context.SaveChanges();
    }
}