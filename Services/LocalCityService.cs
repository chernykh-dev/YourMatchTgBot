using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class LocalCityService : ICityService
{
    private readonly List<City> _cities = new();

    public City? GetCityByName(string name)
    {
        return _cities.FirstOrDefault(city => city.Name == name);
    }

    public City AddCity(string name)
    {
        var newId = _cities.Last().Id + 1;

        var newCity = new City { Id = newId, Name = name, DisplayName = name };
        
        _cities.Add(newCity);

        return newCity;
    }

    public City AddCity(City city)
    {
        _cities.Add(city);

        return city;
    }

    public City? GetCity(long cityId)
    {
        return _cities.FirstOrDefault(city => city.Id == cityId);
    }
}