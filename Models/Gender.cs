namespace YourMatchTgBot.Models;

[Flags]
public enum Gender
{
    Undefined = 0,
    Man,
    Women,
    All = Man | Women,
}