using Bajtpik.Bajtpik3;

namespace Bajtpik.Adapters;

public class AuthorAdapter2 : IAuthor
{
    private readonly IAuthor3 _author3;

    public AuthorAdapter2(IAuthor3 author3)
    {
        _author3 = author3;
    }


    public void PrintAuthor()
    {
        _author3.PrintAuthor();
    }
}

public class NewspaperAdapter2 : INewspaper
{
    private readonly INewspaper3 _newspaper3;

    public NewspaperAdapter2(INewspaper3 newspaper3)
    {
        _newspaper3 = newspaper3;
    }


    public void PrintNewspaper()
    {
        _newspaper3.PrintNewspaper();
    }
}

public class BookAdapter2 : IBook
{
    private readonly IBook3 _book3;
    private readonly List<Author3> authors;

    public BookAdapter2(IBook3 book3, List<Author3> authors)
    {
        _book3 = book3;
        this.authors = authors;
    }

    public void PrintBook()
    {
        _book3.PrintBook(authors);
    }

    public void PrintBookAuthorBornAfter1970()
    {
        if (authors.Any(author => author.GetBirthYear() > 1970))
            _book3.PrintBook(authors);
    }
}

public class BoardgameAdapter2 : IBoardgame
{
    private readonly IBoardgame3 _boardgame3;
    private readonly List<Author3> authors;

    public BoardgameAdapter2(IBoardgame3 boardgame3, List<Author3> authors)
    {
        _boardgame3 = boardgame3;
        this.authors = authors;
    }

    public void PrintBoardgame()
    {
        _boardgame3.PrintBoardgame(authors);
    }

    public void PrintBoardgameAuthorBornAfter1970()
    {
        if (authors.Any(author => author.GetBirthYear() > 1970))
            _boardgame3.PrintBoardgame(authors);
    }
}