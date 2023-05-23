namespace Bajtpik.Bajtpik3;

public class Book3
{
    private readonly GlobalData _book;


    public Book3(GlobalData book, string title, List<int> authorKey, int year, int pageCount)
    {
        _book = book;
        _book.AddBook(title, authorKey, year, pageCount);
    }

    public Book3(GlobalData data, string title, List<Author?> authors, int year, int pageCount)
    {
        _book = data;
        Title = title;
        Year = year;
        PageCount = pageCount;
        Authors = authors;

        _book.AddBook(this);
        foreach (var author in authors.Where(author => author != null))
            _book.AddAuthor(author?.Name!, author.Surname!, author.BirthYear, author.Nickname!);
    }

    public string Title { get; set; }
    public int Year { get; set; }
    public int PageCount { get; set; }
    public List<Author?> Authors { get; set; } = new();
}