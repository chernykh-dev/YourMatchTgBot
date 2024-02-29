using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public interface IInterestService
{
    public Interest? GetInterestById(long id);

    public List<Interest> GetInterestsByIds(params long[] ids);

    public List<Interest> GetInterests();

    public Interest? GetInterestByName(string name);
}