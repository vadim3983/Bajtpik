namespace Bajtpik;

public class Book : IPublication, IBook
{
    public string Title { get; set; }
    public List<Author> Authors { get; set; }
    public int Year { get; set; }
    public int? PageCount { get; set; }

    public void PrintBook()
    {
        Console.WriteLine(Title + " , " + Year + " , " + PageCount + " , " +
                          Authors.Select(x => x.Name + " " + x.Surname).Aggregate((x, y) => x + ", " + y));
    }
}