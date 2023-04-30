namespace Bajtpik;

public class Newspaper : IPublication, INewspaper
{
    public void PrintNewspaper()
    {
        Console.WriteLine(Title + ", " + Year + ", " + PageCount);
    }

    public string? Title { get; set; }
    public int? Year { get; set; }
    public int? PageCount { get; set; }
}