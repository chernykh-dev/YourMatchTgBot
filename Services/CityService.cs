using YourMatchTgBot.Models;

namespace YourMatchTgBot.Services;

public class CityService : ICityService
{
    private readonly ApplicationDbContext _context;

    public CityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public City? GetCityByName(string name)
    {
        return _context.Cities.FirstOrDefault(city => city.Name == name);
    }

    public City AddCity(string name)
    {
        var cityEntry = _context.Cities.Add(new City { Name = name });

        _context.SaveChanges();

        return cityEntry.Entity;
    }

    public City AddCity(City city)
    {
        var cityEntry = _context.Cities.Add(city);

        _context.SaveChanges();
        
        return cityEntry.Entity;
    }

    public City? GetCity(long cityId)
    {
        return _context.Cities.Find(cityId);
    }
}