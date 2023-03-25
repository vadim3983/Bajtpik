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

public class BoardGamesAdapter : IBoardgame
{
    private readonly Author2 _author2;
    private readonly Boardgame2 _boardgame2;
    private int _id = 1;

    public BoardGamesAdapter(Boardgame2 boardgame2, Author2 author2)
    {
        _boardgame2 = boardgame2;
        _author2 = author2;
    }

    public void PrintBoardgame()
    {
        _boardgame2.PrintBoardgame2(_id, _author2);
        _id++;
    }

    public void PrintBoardgameAuthorBornAfter1970()
    {
        if (_author2.GetBirthYear() > 1970)
            _boardgame2.PrintBoardgame2(_id, _author2);
    }
}

public class BooksAdapter : IBook
{
    private readonly Author2 _author2;
    private readonly Book2 _book2;
    private int _id = 1;

    public BooksAdapter(Book2 book2, Author2 author2)
    {
        _book2 = book2;
        _author2 = author2;
    }

    public void PrintBookAuthorBornAfter1970()
    {
        if (_author2.GetBirthYear() > 1970)
            _book2.PrintBook2(_id, _author2);
    }

    public void PrintBook()
    {
        _book2.PrintBook2(_id, _author2);
        _id++;
    }
}

public class NewspaperAdapter : INewspaper
{
    private readonly INewspaper2 _newspaper2;
    private int _id = 1;


    public NewspaperAdapter(INewspaper2 newspaper2)
    {
        _newspaper2 = newspaper2;
    }

    public void PrintNewspaper()
    {
        _newspaper2.PrintNewspaper2(_id);
        _id++;
    }
}