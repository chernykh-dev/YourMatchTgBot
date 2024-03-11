using System.ComponentModel.DataAnnotations;

namespace YourMatchTgBot.Models;

public class City
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string TranslatedName { get; set; } = null!;

    public string TranslatedDisplayName { get; set; } = null!;
}