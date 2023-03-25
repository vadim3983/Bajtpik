namespace Bajtpik.Bajtpik3;

public class Book3 : IBook3
{
    private readonly Dictionary<int, string> _book = new();
    private readonly int _pageCount;

    private readonly string _title;
    private readonly int _year;

    public Book3(string title, int year, int pageCount)
    {
        _title = title;
        _year = year;
        _pageCount = pageCount;

        _book[title.GetHashCode()] = title;
        _book[year.ToString().GetHashCode()] = year.ToString();
        _book[pageCount.ToString().GetHashCode()] = pageCount.ToString();
    }

    public void PrintBook(List<Author3> authors)
    {
        var title = _book[_title.GetHashCode()];
        var year = _book[_year.ToString().GetHashCode()];
        var pageCount = _book[_pageCount.ToString().GetHashCode()];

        var authorNames = string.Join(", ", authors.Select(author => $"{author.GetName()} {author.GetSurname()}"));
        if (authors.Count > 1) authorNames = $"[{authorNames}]";

        var output = $"{title}, {authorNames}, {year}, {pageCount}";

        Console.WriteLine(output);
    }
}