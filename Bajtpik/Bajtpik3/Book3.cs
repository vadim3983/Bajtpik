namespace Bajtpik.Bajtpik3;

public class Book3
{
    private readonly GlobalData _book;

    public Book3(GlobalData book, string title, List<int> authorKey, int year, int pageCount)
    {
        _book = book;
        _book.AddBook(title, authorKey, year, pageCount);
    }
}