using Microsoft.EntityFrameworkCore;
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
        return _context.Users
            .Include(u => u.Interests)
            .Include(u => u.Photos)
            .Include(u => u.City)
            .Include(u => u.TemporaryInterests)
            .Include(u => u.TemporaryPhotos)
            .FirstOrDefault(u => u.Id == id);
    }

    public User? FindUserForUser(User user)
    {
        return _context.Users
            .Include(u => u.Interests)
            .Include(u => u.Photos)
            .Include(u => u.City)
            .Include(u => u.TemporaryInterests)
            .Include(u => u.TemporaryPhotos)
            .FirstOrDefault(u => u.Id != user.Id);
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