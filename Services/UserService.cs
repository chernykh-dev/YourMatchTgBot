using System.Collections;
using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class UserService
{
    private readonly InterestService _interestService;

    private List<User> _users;

    public UserService(InterestService interestService)
    {
        _interestService = interestService;

        _users = new List<User>()
        {
            new()
            {
                Id = 472106852L + 10,
                Name = "Артём",
                Age = 23,
                City = "Белгород",
                ZodiacSign = "Дева",
                Height = 175,
                Education = "Высшее",
                Interests = _interestService.GetInterestsByIds(2, 3, 5, 7)
            }
        };
    }

    public User? GetUserById(long id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User AddUser(User user)
    {
        _users.Add(user);

        return user;
    }
}