using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class CityService : ICityService
{
    private readonly List<City> _cities = new()
    {
        new City { Id = 1, Name = "Belgorod" }
    };

    public City? GetCityByName(string name)
    {
        return _cities.FirstOrDefault(city => city.Name == name);
    }

    public City AddCity(string name)
    {
        var newId = _cities.Last().Id + 1;

        var newCity = new City { Id = newId, Name = name };
        
        _cities.Add(newCity);

        return newCity;
    }
}