using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public interface IInterestService
{
    public Interest? GetInterestById(long id);

    public List<Interest> GetInterestsByIds(params long[] ids);

    public List<Interest> GetInterests();

    public Interest? GetInterestByName(string name);
    
    public static List<long> GetInterestsFlags(int interestsFlags)
    {
        var ids = new List<long>();

        for (var k = 0; k < 32; k++)
        {
            var mask = 1 << k;
            var maskedId = interestsFlags & mask;
            
            if (maskedId > 0)
                ids.Add(maskedId);
        }

        return ids;
    }
}