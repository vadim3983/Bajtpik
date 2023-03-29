namespace Bajtpik.Bajtpik3;

public class Author3
{
    private readonly GlobalData _author;

    public Author3(GlobalData author, string name, string surname, int birthYear, string nickname)
    {
        if (birthYear <= 0)
        {
            Console.WriteLine("Author information is incomplete. Birth year must be greater than 0.");
            return;
        }

        _author = author;
        _author.AddAuthor(name, surname, birthYear, nickname);
    }
}