using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public interface IUserService
{
    public User? GetUserById(long id);

    public User? FindUserForUser(User user);

    public User AddUser(User user);
    
    public void DeleteUser(User user);
}