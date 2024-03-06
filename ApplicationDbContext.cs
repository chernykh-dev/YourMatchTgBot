using Microsoft.EntityFrameworkCore;
using YourMatchTgBot.Models;

namespace YourMatchTgBot;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Interest> Interests { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public ApplicationDbContext()
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
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
    }
}