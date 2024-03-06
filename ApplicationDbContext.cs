using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;

namespace YourMatchTgBot;

public class ApplicationDbContext : DbContext
{
    private const string TEST_DATA_FILE_PATH = "Resources/test-data.csv";
    private static readonly Random Random = new (1112452657);
    
    private static readonly List<City> TestCities = new ()
    {
        new City { Id = 150993485, Name = "Saint Petersburg", DisplayName = "Saint Petersburg, Northwestern Federal District, Russia" },
        new City { Id = 171572505, Name = "Belgorod", DisplayName = "Belgorod, Belgorodsky District, Belgorod Oblast, Central Federal District, Russia" },
        new City { Id = 171830246, Name = "Kursk", DisplayName = "Kursk, Kursk Oblast, Central Federal District, 305000, Russia" },
        new City { Id = 171835519, Name = "Stroitel", DisplayName = "Stroitel, Yakovlevsky District, Belgorod Oblast, Central Federal District, Russia" },
        new City { Id = 172182020, Name = "Voronezh", DisplayName = "Voronezh, Voronezh Oblast, Central Federal District, Russia" },
        new City { Id = 174007564, Name = "Penza", DisplayName = "Penza, Penza Oblast, Volga Federal District, Russia" },
        new City { Id = 174159235, Name = "Bryansk", DisplayName = "Bryansk, Bryansk Oblast, Central Federal District, Russia" },
        new City { Id = 174912365, Name = "Lipetsk", DisplayName = "Lipetsk, Lipetsk Oblast, Central Federal District, 398000, Russia" },
        new City { Id = 176023942, Name = "Izhevsk", DisplayName = "Izhevsk, Udmurtia, Volga Federal District, Russia" },
        new City { Id = 179737040, Name = "Samara", DisplayName = "Samara, Samara Oblast, Volga Federal District, 443028, Russia" },
        new City { Id = 206154094, Name = "Moscow", DisplayName = "Moscow, Central Federal District, Russia" }
        
    };

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Interest> Interests { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public ApplicationDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Db/Lite.db");
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

    private void AddTestData(ModelBuilder builder)
    {
        using (var parser = new TextFieldParser(TEST_DATA_FILE_PATH))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");

            // Read first row (headers).
            parser.ReadFields();
            
            while (!parser.EndOfData)
            {
                var user = new User();
                
                var fields = parser.ReadFields();

                user.Id = -long.Parse(fields[0]);
                user.Name = fields[1];
                user.Age = GenerateRandomAge(fields[2]);
                
                Enum.TryParse<Gender>(fields[3], out var userGender);
                user.Gender = userGender;
                
                Enum.TryParse<Gender>(fields[4], out var userPartnerGender);
                user.PartnerGender = userPartnerGender;

                user.InterestsFlags = int.Parse(fields[5]) | int.Parse(fields[6]) | int.Parse(fields[7]);

                user.Height = int.Parse(fields[8]);

                var cityId = long.Parse(fields[9]);
                user.CityId = cityId;

                builder.Entity<UserMedia>().HasData(new UserMedia
                {
                    UserId = user.Id,
                    MediaFileId = fields[10],
                    MediaType = MessageType.Photo
                });

                user.Description = fields[11];

                builder.Entity<User>().HasData(user);
            }
        }
    }

    private static short GenerateRandomAge(string csvAgeField)
    {
        var minMaxAge = csvAgeField.Split('-');

        return (short)Random.Next(short.Parse(minMaxAge[0]), short.Parse(minMaxAge[1]));
    }
}