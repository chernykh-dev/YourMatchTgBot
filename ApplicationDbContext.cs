using Microsoft.EntityFrameworkCore;
using YourMatchTgBot.Models;

namespace YourMatchTgBot;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Interest> Interests { get; set; }
    
    public DbSet<City> Cities { get; set; }

    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
        // Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Interest>().HasData(
            new Interest { Id = 1, Name = "📚" }, 
            new Interest { Id = 2, Name = "🎲" },
            new Interest { Id = 3, Name = "🚶" }, 
            new Interest { Id = 4, Name = "💃" },
            new Interest { Id = 5, Name = "🎞" }, 
            new Interest { Id = 6, Name = "🏅" },
            new Interest { Id = 7, Name = "💻" }, 
            new Interest { Id = 8, Name = "🚙" },
            new Interest { Id = 9, Name = "🏔" }, 
            new Interest { Id = 10, Name = "🍲" },
            new Interest { Id = 11, Name = "🎧" }, 
            new Interest { Id = 12, Name = "🍳" },
            new Interest { Id = 13, Name = "🛍" }
        );

        builder.Entity<City>().HasData(
            new City { Id = 1, Name = "Belgorod" }
        );
    }
}