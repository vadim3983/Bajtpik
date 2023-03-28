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
    private readonly GlobalData _globalData;

    private GlobalData Book { get; set; }
    public object Author { get; set; }

    public BookAdapter2(IGlobalData book3)
    {
        _book3 = book3;
        _globalData = new GlobalData();
    }

    public void PrintBook()
    {
        _book3.PrintAllBooks();
    }

    public void PrintAllBooksByAuthorsBornAfter(int yearThreshold = 1970)
{
    Console.WriteLine("Printing books with authors born after {0}", yearThreshold);

    Dictionary<int, string> bookDictionary = _book3.GetBookDictionary();
    Dictionary<int, string> authorDictionary = _book3.GetAuthorDictionary();

    foreach (KeyValuePair<int, string> bookEntry in bookDictionary)
    {
        Console.WriteLine("Processing book: {0}", bookEntry.Value);

        string[] bookDetails = bookEntry.Value.Split(',');
        string authorsString = bookDetails[1].Trim();

        bool bookHasAuthorBornAfterThreshold = false;

        var authors = authorsString.StartsWith("[") && authorsString.EndsWith("]")
            ? authorsString.Substring(1, authorsString.Length - 2).Split(',').Select(a => a.Trim())
            : new List<string> { authorsString };

        foreach (string author in authors)
        {
            string[] authorNameParts = author.Trim().Split(' ');
            string firstName = authorNameParts[0];
            string lastName = authorNameParts.Length > 1 ? authorNameParts[1] : "";

            var matchingAuthors = authorDictionary
                .Where(a => a.Value.StartsWith($"{firstName} {lastName}", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var matchingAuthor in matchingAuthors)
            {
                string[] authorDetails = matchingAuthor.Value.Split(',');
                if (authorDetails.Length < 3) 
                {
                    continue;
                }

                int birthYear = int.Parse(authorDetails[2].Trim());

                if (birthYear > yearThreshold)
                {
                    Console.WriteLine("Found author '{0}' born in {1}", author, birthYear);
                    bookHasAuthorBornAfterThreshold = true;
                    break;
                }
            }

            if (bookHasAuthorBornAfterThreshold) break;
        }

        if (bookHasAuthorBornAfterThreshold)
        {
            Console.WriteLine(bookEntry.Value);
        }
    }
}










}


public class BoardgameAdapter2 : IBoardgame
{
    private readonly IGlobalData _boardgame3;
    private readonly List<Author3> authors;

    public BoardgameAdapter2(IGlobalData boardgame3)
    {
        _boardgame3 = boardgame3;
    }

    public void PrintBoardgame()
    {
        _boardgame3.PrintAllBoardgames();
    }
    
}
