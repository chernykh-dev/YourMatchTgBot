using Geolocation;
using Microsoft.EntityFrameworkCore;
using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class UserMatchingService
{
    public const int USERS_BY_SEARCH = 20;

    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserMatchingService> _logger;

    public UserMatchingService(ApplicationDbContext context, ILogger<UserMatchingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public List<User> MatchForUserByCity(User userForMatch)
    {
        var users = _context.Users
            .Where(u => u.City == userForMatch.City)
            .Skip(userForMatch.SearchOffset)
            .Take(USERS_BY_SEARCH)
            // .Include(u => u.Photos)
            // .Include(u => u.City)
            .ToList();

        return SortUsersFor(userForMatch, users).ToList();
    }

    public List<User> MatchForUserByDistance(User userForMatch)
    {
        var users = _context.Users
            // .Include(u => u.City)
            // .Include(u => u.Photos)
            .AsEnumerable()
            .Where(u => u.Id != userForMatch.Id)
            .Where(u => (u.Gender.Value & userForMatch.PartnerGender.Value) > 0 && (userForMatch.Gender.Value & u.PartnerGender.Value) > 0)
            .Select(u => new
            {
                User = u,
                Distance = GeoCalculator.GetDistance(
                    userForMatch.Latitude.Value, userForMatch.Longitude.Value,
                    u.Latitude.Value, u.Longitude.Value, 1, DistanceUnit.Kilometers),
                Probability = UsersProbability(userForMatch, u)
            })
            .OrderBy(u => u.Distance)
            .ThenByDescending(u => u.Probability)
            .Skip(userForMatch.SearchOffset)
            .Take(USERS_BY_SEARCH)
            .Select(u => u.User)
            .ToList();

        return users;
    }

    private IEnumerable<User> SortUsersFor(User userForMatch, IEnumerable<User> users)
    {
        return users.OrderByDescending(user => UsersProbability(userForMatch, user));
    }

    public static double UsersProbability(User userForMatch, User user)
    {
        var commonInterests = userForMatch.InterestsFlags & user.InterestsFlags;
        var commonInterestsCount = IInterestService.GetInterestsFlags(commonInterests).Count;
        var interestsProbability = (double)commonInterestsCount / 3;

        var distance = GeoCalculator.GetDistance(
            userForMatch.Latitude.Value, userForMatch.Longitude.Value,
            user.Latitude.Value, user.Longitude.Value,
            1, DistanceUnit.Kilometers);
        var distanceProbability = Math.Exp(-0.0001 * distance);

        var ageDifference = Math.Abs(userForMatch.Age.Value - user.Age.Value);
        var ageProbability = 1 - (double)ageDifference / 109;

        var commonProbability = distanceProbability * 0.35 + interestsProbability * 0.4 + ageProbability * 0.25;

        return commonProbability;
    }
}
