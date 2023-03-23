using Bajtpik.Bajtpik2;

namespace Bajtpik.Adapters;

public class BooksAdapter : IBook
{
    private readonly Book2 _book2;
    private readonly Author2 _author2;
    private int _id = 1;

    public BooksAdapter(Book2 book2, Author2 author2)
    {
        _book2 = book2;
        _author2 = author2;
    }

    public void PrintBook()
    {
        _book2.PrintBook2(_id, _author2);
        _id++;
    }
}