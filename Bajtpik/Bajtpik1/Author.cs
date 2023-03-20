namespace Bajtpik;

public class Author : IAuthor
{
    public string Name;
    public string Surname;
    public int BirthYear;
    public string? Nickname;

    public void PrintAuthor()
    {
        Console.WriteLine(Name + " , " + Surname + " , " + BirthYear + " , " + Nickname);
    }
}