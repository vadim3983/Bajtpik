using Bajtpik.Bajtpik3;
using Bajtpik.Bajtpik3.IBajtpik3;

namespace Bajtpik.Adapters;

public class AuthorAdapter2 : IAuthor
{
    private readonly IGlobalData _author3;

    public AuthorAdapter2(IGlobalData author3)
    {
        _author3 = author3;
    }


    public void PrintAuthor()
    {
        _author3.PrintAllAuthors();
    }
}

public class NewspaperAdapter2 : INewspaper
{
    private readonly IGlobalData _newspaper3;

    public NewspaperAdapter2(IGlobalData newspaper3)
    {
        _newspaper3 = newspaper3;
    }


    public void PrintNewspaper()
    {
        _newspaper3.PrintAllNewspapers();
    }
}

public class BookAdapter2 : IBook
{
    private readonly IGlobalData _book3;

    public BookAdapter2(IGlobalData book3)
    {
        _book3 = book3;
        new GlobalData();
    }

    public void PrintBook()
    {
        _book3.PrintAllBooks();
    }

    public void PrintBookAuthorBornAfter1970()
    {
        var bookDictionary = _book3.GetBookDictionary();
        var authorDictionary = _book3.GetAuthorDictionary();

        foreach (var kvp in bookDictionary)
        {
            var bookInfo = kvp.Value;
            var bookDetails = bookInfo.Split(',');

            if (bookDetails.Length >= 3)
            {
                var authorsString = bookDetails[1].Trim();
                var isAnyAuthorBornAfter1970 = false;

                if (authorsString.StartsWith("[") && authorsString.EndsWith("]"))
                {
                    authorsString = authorsString.Substring(1, authorsString.Length - 2);
                    var authorNames = authorsString.Split(',');

                    foreach (var authorName in authorNames)
                    {
                        var nameParts = authorName.Trim().Split(' ');
                        var firstName = nameParts[0];
                        var lastName = nameParts.Length > 1 ? nameParts[1] : "";
                        var authorKey = _book3.GetAuthorKey(firstName, lastName);

                        if (authorKey != -1 && IsAuthorBornAfter1970(authorKey))
                        {
                            isAnyAuthorBornAfter1970 = true;
                            break;
                        }
                    }
                }
                else
                {
                    var nameParts = authorsString.Split(' ');
                    var firstName = nameParts[0];
                    var lastName = nameParts.Length > 1 ? nameParts[1] : "";
                    var authorKey = _book3.GetAuthorKey(firstName, lastName);

                    if (authorKey != -1 && IsAuthorBornAfter1970(authorKey)) isAnyAuthorBornAfter1970 = true;
                }

                if (isAnyAuthorBornAfter1970) Console.WriteLine("{0}", kvp.Value);
            }
        }
    }

    private bool IsAuthorBornAfter1970(int authorKey)
    {
        var authorDictionary = _book3.GetAuthorDictionary();

        if (authorDictionary.TryGetValue(authorKey, out var authorInfo))
        {
            var authorDetails = authorInfo.Split(' ');
            if (authorDetails.Length >= 3)
            {
                var authorBirthYear = int.Parse(authorDetails[2].Trim());

                if (authorBirthYear > 1970)
                    return true;
                return false;
            }

            Console.WriteLine("Author information is incomplete. Birth year information is missing.");
            return false;
        }

        Console.WriteLine($"Author with key {authorKey} not found. Please provide a valid authorKey.");
        return false;
    }
}

public class BoardgameAdapter2 : IBoardgame
{
    private readonly IGlobalData _boardgame3;


    public BoardgameAdapter2(IGlobalData boardgame3)
    {
        _boardgame3 = boardgame3;
    }

    public void PrintBoardgame()
    {
        _boardgame3.PrintAllBoardgames();
    }


    public void PrintBoardgameAuthorBornAfter1970()
    {
        var bookDictionary = _boardgame3.GetBoardGameDictionary();
        var authorDictionary = _boardgame3.GetAuthorDictionary();

        foreach (var kvp in bookDictionary)
        {
            var boardgameInfo = kvp.Value;
            var boardgameDetails = boardgameInfo.Split(',');

            if (boardgameDetails.Length < 5) continue;
            var authorsString = boardgameDetails[4].Trim();
            var isAnyAuthorBornAfter1970 = false;

            if (authorsString.StartsWith("[") && authorsString.EndsWith("]"))
            {
                authorsString = authorsString.Substring(1, authorsString.Length - 2);
                var authorNames = authorsString.Split(',');

                foreach (var authorName in authorNames)
                {
                    var nameParts = authorName.Trim().Split(' ');
                    var firstName = nameParts[0];
                    var lastName = nameParts.Length > 1 ? nameParts[1] : "";
                    var authorKey = _boardgame3.GetAuthorKey(firstName, lastName);

                    if (authorKey != -1 && IsAuthorBornAfter1970(authorKey))
                    {
                        isAnyAuthorBornAfter1970 = true;
                        break;
                    }
                }
            }
            else
            {
                var nameParts = authorsString.Split(' ');
                var firstName = nameParts[0];
                var lastName = nameParts.Length > 1 ? nameParts[1] : "";
                var authorKey = _boardgame3.GetAuthorKey(firstName, lastName);

                if (authorKey != -1 && IsAuthorBornAfter1970(authorKey)) isAnyAuthorBornAfter1970 = true;
            }

            if (isAnyAuthorBornAfter1970) Console.WriteLine("{0}", kvp.Value);
        }
    }


    private bool IsAuthorBornAfter1970(int authorKey)
    {
        var authorDictionary = _boardgame3.GetAuthorDictionary();

        if (authorDictionary.TryGetValue(authorKey, out var authorInfo))
        {
            var authorDetails = authorInfo.Split(' ');
            if (authorDetails.Length >= 3)
            {
                var authorBirthYear = int.Parse(authorDetails[2].Trim());

                return authorBirthYear > 1970;
            }

            Console.WriteLine("Author information is incomplete. Birth year information is missing.");
            return false;
        }

        Console.WriteLine($"Author with key {authorKey} not found. Please provide a valid authorKey.");
        return false;
    }
}