namespace Bajtpik.Bajtpik3;

public class Author3
{
    private readonly GlobalData _author;

    public Author3(GlobalData author, string name, string surname, int birthYear, string nickname)
    {
        _author = author;
        _author.AddAuthor(name, surname, birthYear, nickname);
    }
}