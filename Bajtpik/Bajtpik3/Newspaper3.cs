namespace Bajtpik.Bajtpik3;

public class Newspaper3 : INewspaper3
{
    private readonly Dictionary<int, string> _Newspaper = new();
    private readonly int _pageCount;
    private readonly string _title;
    private readonly int _year;

    public Newspaper3(string title, int year, int pageCount)
    {
        _title = title;
        _year = year;
        _pageCount = pageCount;

        _Newspaper[title.GetHashCode()] = title;
        _Newspaper[year.ToString().GetHashCode()] = year.ToString();
        _Newspaper[pageCount.ToString().GetHashCode()] = pageCount.ToString();
    }

    public void PrintNewspaper()
    {
        var title = _Newspaper[_title.GetHashCode()];
        var year = _Newspaper[_year.ToString().GetHashCode()];
        var pageCount = _Newspaper[_pageCount.ToString().GetHashCode()];

        var output = $"{title}, {year}, {pageCount}";

        Console.WriteLine(output);
    }
}