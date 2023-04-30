namespace Bajtpik;

public class Book : IBook
{
    public List<Author?> Authors { get; set; } = new(); 


    public void PrintBook()
    {
        Console.WriteLine(
            $"{Title} , {(Authors.Count > 1 ? "[" : "")}{string.Join(", ", Authors.Select(a => $"{a.Name} {a.Surname}"))}{(Authors.Count > 1 ? "]" : "")}, {Year} , {PageCount}");
    }

    public void PrintBookAuthorBornAfter1970()
    {
        if (Authors.Any(author => author.BirthYear > 1970)) PrintBook();
    }

    public string? Title= "";
    public int? Year= 0;
    public int? PageCount = null;
}