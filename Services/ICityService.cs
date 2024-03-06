using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public interface ICityService
{
    public City? GetCityByName(string name);

    public City AddCity(string name);
    
    public City AddCity(City city);
    
    City? GetCity(long cityId);
}