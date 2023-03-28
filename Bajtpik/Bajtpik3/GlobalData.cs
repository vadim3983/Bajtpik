using Bajtpik.Bajtpik3.IBajtpik3;

namespace Bajtpik.Bajtpik3;

public class GlobalData : IGlobalData
{
    private readonly Dictionary<int, string> _newspaper;
    private readonly Dictionary<int,string> _author;
    private readonly Dictionary<int,string> _book;
    private readonly Dictionary<int, string> _boardgame;


    public GlobalData()
    {
        _newspaper = new Dictionary<int, string>();
        _author = new Dictionary<int, string>();
        _book = new Dictionary<int, string>();
        _boardgame = new Dictionary<int, string>();
    }
    
    public void AddAuthor(string name, string surname, int birthYear, string nickname)
    {
        var value = nickname == "" ? $"{name} {surname}, {birthYear}" : $"{name} {surname}, {birthYear}, {nickname}";
        var key = (name + surname + birthYear.ToString()).GetHashCode();
        _author.Add(key, value);
    }
    
    public void AddNewspaper(string title, int year, int pageCount)
    {
        var value = $"{title}, {year}, {pageCount} ";
        var key = (title + year + pageCount).GetHashCode();
        _newspaper.Add(key, value);
    }
    
    public void AddBook(string title, List<int> authorKeys, int year, int pageCount)
    {
        List<string> authorNames = new List<string>();

        foreach (int authorKey in authorKeys)
        {
            if (_author.TryGetValue(authorKey, out var authorInfo))
            {
                string[] authorDetails = authorInfo.Split(',');
                string authorName = authorDetails[0];
                authorNames.Add(authorName);
            }
            else
            {
                Console.WriteLine($"Author with key {authorKey} not found. Please provide a valid authorKey.");
            }
        }

        if (authorNames.Count > 0)
        {
            string authors = authorNames.Count > 1 ? $"[{string.Join(", ", authorNames)}]" : authorNames[0];
            var value = $"{title}, {authors}, {year}, {pageCount}";
            var key = (title + string.Join("", authorNames) + year + pageCount).GetHashCode();
            _book.Add(key, value);
        }
        else
        {
            Console.WriteLine("No valid authors found. Please provide at least one valid authorKey.");
        }
    }

    public void AddBoardgame(string title, int minPlayers, int maxPlayers, int difficulty, List<int> authorKeys)
    {
        List<string> authorNames = new List<string>();

        foreach (int authorKey in authorKeys)
        {
            if (_author.TryGetValue(authorKey, out var authorInfo))
            {
                string[] authorDetails = authorInfo.Split(',');
                string authorName = authorDetails[0];
                authorNames.Add(authorName);
            }
            else
            {
                Console.WriteLine($"Author with key {authorKey} not found. Please provide a valid authorKey.");
            }
        }

        if (authorNames.Count > 0)
        {
            string authors = authorNames.Count > 1 ? $"[{string.Join(", ", authorNames)}]" : authorNames[0];
            var value = $"\"{title}\", {minPlayers}, {maxPlayers}, {difficulty}, {authors}";
            var key = (title + minPlayers + maxPlayers + difficulty + string.Join("", authorNames) ).GetHashCode();
            _boardgame.Add(key, value);
        }
        else
        {
            Console.WriteLine("No valid authors found. Please provide at least one valid authorKey.");
        }
    }
    
    public int GetAuthorKey(string name, string surname, int? birthYear = null)
    {
        int searchKey;

        if (birthYear.HasValue)
        {
            searchKey = (name + surname + birthYear.Value.ToString()).GetHashCode();
        }
        else
        {
            foreach (var authorEntry in _author)
            {
                string[] authorDetails = authorEntry.Value.Split(',');
                string firstName = authorDetails[0].Trim();
                string lastName = authorDetails.Length > 1 ? authorDetails[1].Trim() : "";

                if (firstName.Equals(name, StringComparison.OrdinalIgnoreCase) && lastName.Equals(surname, StringComparison.OrdinalIgnoreCase))
                {
                    return authorEntry.Key;
                }
            }

            return -1;
        }

        if (_author.ContainsKey(searchKey))
        {
            return searchKey;
        }
        return -1;
    }

    
    public void PrintAllNewspapers()
    {
        foreach (KeyValuePair<int, string> kvp in _newspaper)
        {
            Console.WriteLine("{0}", kvp.Value);
        }
    }
    
    public void PrintAllAuthors()
    {
        foreach (KeyValuePair<int, string> kvp in _author)
        {
            Console.WriteLine("{0}", kvp.Value);
        }
    }
    
    public void PrintAllBooks()
    {
        foreach (KeyValuePair<int, string> kvp in _book)
        {
            Console.WriteLine("{0}", kvp.Value);
        }
    }
    
    public void PrintAllBoardgames()
    {
        foreach (KeyValuePair<int, string> kvp in _boardgame)
        {
            Console.WriteLine("{0}", kvp.Value);
        }
    }

    public Dictionary<int, string> GetBookDictionary()
    {
        return _book;
    }

    public Dictionary<int, string> GetAuthorDictionary()
    {
        return _author;
    }
    
}

