using Geolocation;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;

namespace YourMatchTgBot;

public class ApplicationDbContext : DbContext
{
    private const string TEST_DATA_FILE_PATH = "Resources/test-data.csv";
    private static readonly Random Random = new (1112452657);
    
    private static readonly List<City> TestCities = new ()
    {
        new City { Id = 150993485,
            Name = "Saint Petersburg", DisplayName = "Saint Petersburg, Northwestern Federal District, Russia",
            TranslatedName = "Санкт-Петербург", TranslatedDisplayName = "Санкт-Петербург, Северо-Западный федеральный округ, Россия"
        },
        new City { Id = 171488446,
            Name = "Belgorod", DisplayName = "Belgorod, Belgorodsky District, Belgorod Oblast, Central Federal District, Russia",
            TranslatedName = "Белгород", TranslatedDisplayName = "Белгород, Белгородский район, Белгородская область, Центральный федеральный округ, Россия"
        },
        new City { Id = 170978781,
            Name = "Kursk", DisplayName = "Kursk, Kursk Oblast, Central Federal District, 305000, Russia",
            TranslatedName = "Курск", TranslatedDisplayName = "Курск, Курская область, Центральный федеральный округ, 305000, Россия"
        },
        new City { Id = 172182020,
            Name = "Voronezh", DisplayName = "Voronezh, Voronezh Oblast, Central Federal District, Russia",
            TranslatedName = "Воронеж", TranslatedDisplayName = "Воронеж, Воронежская область, Центральный федеральный округ, Россия"
        },
        new City { Id = 172497247,
            Name = "Penza", DisplayName = "Penza, Penza Oblast, Volga Federal District, Russia",
            TranslatedName = "Пенза", TranslatedDisplayName = "Пенза, Пензенская область, Приволжский федеральный округ, Россия"
        },
        new City { Id = 174159235,
            Name = "Bryansk", DisplayName = "Bryansk, Bryansk Oblast, Central Federal District, Russia",
            TranslatedName = "Брянск", TranslatedDisplayName = "Брянск, Брянская область, Центральный федеральный округ, Россия"
        },
        new City { Id = 177211127,
            Name = "Izhevsk", DisplayName = "Izhevsk, Udmurtia, Volga Federal District, Russia",
            TranslatedName = "Ижевск", TranslatedDisplayName = "Ижевск, Удмуртия, Приволжский федеральный округ, Россия"
        },
        new City { Id = 179737040,
            Name = "Samara", DisplayName = "Samara, Samara Oblast, Volga Federal District, 443028, Russia",
            TranslatedName = "Самара", TranslatedDisplayName = "Самара, Самарская область, Приволжский федеральный округ, 443028, Россия"
        },
        new City { Id = 174706474,
            Name = "Moscow", DisplayName = "Moscow, Central Federal District, Russia",
            TranslatedName = "Москва", TranslatedDisplayName = "Москва, Центральный федеральный округ, Россия"
        }
    };

    private static readonly Dictionary<long, Coordinate> TestCoordinates = new()
    {
        { 150993485, new Coordinate(59.93873200, 30.3162290) },
        { 171488446, new Coordinate(50.59555950, 36.5873394) },
        { 170978781, new Coordinate(51.72703565, 36.192247956921115) },
        { 172182020, new Coordinate(51.66059820, 39.2005858) },
        { 172497247, new Coordinate(53.19378360, 45.006741250609664) },
        { 174159235, new Coordinate(53.24237780, 34.3668288) },
        { 177211127, new Coordinate(56.86051745, 53.197730742455306) },
        { 179737040, new Coordinate(53.19862700, 50.1139870) },
        { 174706474, new Coordinate(55.75054120, 37.6174782) }
    };

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Interest> Interests { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public ApplicationDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            // .UseSqlite("Data Source=Db/Lite.db");
            .UseNpgsql("Host=localhost;Port=5432;Database=ymdb;Username=root;Password=root");

        // AddTestData(null);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Interest>().HasData(
            new Interest { Id = 1, Name = "📚" }, 
            new Interest { Id = 2, Name = "🎲" },
            new Interest { Id = 4, Name = "🚶" }, 
            new Interest { Id = 8, Name = "💃" },
            new Interest { Id = 16, Name = "🎞" }, 
            new Interest { Id = 32, Name = "🏅" },
            new Interest { Id = 64, Name = "💻" }, 
            new Interest { Id = 128, Name = "🚙" },
            new Interest { Id = 256, Name = "🏔" }, 
            new Interest { Id = 512, Name = "🍲" },
            new Interest { Id = 1024, Name = "🎧" }, 
            new Interest { Id = 2048, Name = "🍳" },
            new Interest { Id = 4096, Name = "🛍" }
        );

        builder.Entity<City>().HasData(TestCities);
        
        AddTestData(builder);
    }

    private async void AddTestData(ModelBuilder builder)
    {
        using var client = new HttpClient();

        var jsonObject = (JObject)JsonConvert.DeserializeObject(await client.Send(new HttpRequestMessage()
        {
            RequestUri = new Uri("https://api.slingacademy.com/v1/sample-data/users?limit=100")
        }).Content.ReadAsStringAsync());

        using var parser = new TextFieldParser(TEST_DATA_FILE_PATH);

        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(";");

        // Read first row (headers).
        parser.ReadFields();

        var i = 0;
        while (!parser.EndOfData)
        {
            var user = new User();

            var fields = parser.ReadFields();

            user.Id = -long.Parse(fields[0]);
            user.Name = fields[1];
            user.Age = GenerateRandomAge(fields[2]);

            Enum.TryParse<Gender>(fields[3], out var userGender);
            user.Gender = userGender;

            var jsonGender = userGender == Gender.Man ? "male" : "female";

            while (jsonObject["users"][i]["gender"].Value<string>() != jsonGender)
                i = (i + 1) % 100;

            Enum.TryParse<Gender>(fields[4], out var userPartnerGender);
            user.PartnerGender = userPartnerGender;

            user.InterestsFlags = int.Parse(fields[5]);
            // user.InterestsFlags = int.Parse(fields[5]) | int.Parse(fields[6]) | int.Parse(fields[7]);

            user.Height = int.Parse(fields[9]);

            var cityId = long.Parse(fields[10]);
            user.CityId = cityId;

            var cityCoordinate = TestCoordinates[cityId];
            user.Latitude = cityCoordinate.Latitude;
            user.Longitude = cityCoordinate.Longitude;

            builder.Entity<UserMedia>().HasData(new UserMedia
            {
                UserId = user.Id,
                MediaFileId = jsonObject["users"][i]["profile_picture"].Value<string>(),
                MediaType = MessageType.Photo
            });

            user.Description = fields[12];

            builder.Entity<User>().HasData(user);

            i = (i + 1) % 100;
        }
    }

    private static short GenerateRandomAge(string csvAgeField)
    {
        var minMaxAge = csvAgeField.Split('-');

        return (short)Random.Next(short.Parse(minMaxAge[0]), short.Parse(minMaxAge[1]));
    }
}