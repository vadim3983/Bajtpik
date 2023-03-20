using Bajtpik.Bajtpik2;

namespace Bajtpik.Adapters;

public class AuthorAdapter : IAuthor
{
    private readonly IAuthor2 _author2;
    private int _id = 1;

    public AuthorAdapter(IAuthor2 author2)
    {
        _author2 = author2;
    }

    public void PrintAuthor()
    {
        _author2.PrintAuthor2(_id);
        _id++;
    }
}