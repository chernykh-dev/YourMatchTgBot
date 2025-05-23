﻿using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services.Local;

public class LocalUserService : IUserService
{
    private readonly IInterestService _interestService;

    private readonly List<User> _users;

    private readonly ILogger<LocalUserService> _logger;

    public LocalUserService(IInterestService interestService, ILogger<LocalUserService> logger)
    {
        _interestService = interestService;
        _logger = logger;

        _users = new List<User>()
        {
            new()
            {
                Id = 472106852L + 10,
                Name = "Артём",
                Age = 23,
                // City = "Белгород",
                ZodiacSign = "Дева",
                Height = 175,
                Education = "Высшее",
                // Interests = _interestService.GetInterestsByIds(2, 3, 5, 7)
            }
        };
        
    }

    public User? GetUserById(long id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User? FindUserForUser(User user)
    {
        throw new NotImplementedException();
    }

    public User AddUser(User user)
    {
        _users.Add(user);

        return user;
    }

    public void DeleteUser(User user)
    {
        _users.Remove(user);
    }
}