using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public interface IUserService
{
    public User? GetUserById(long id);

    public User AddUser(User user);
}