namespace Bajtpik;

public class Author : IAuthor
{
    public int BirthYear;
    public string Name;
    public string? Nickname;
    public string Surname;

    public void PrintAuthor()
    {
        Console.Write($"{Name} {Surname}, {BirthYear}");
        if (!string.IsNullOrEmpty(Nickname)) Console.Write($", {Nickname}");
        Console.WriteLine();
    }
}