using Bajtpik.Bajtpik3.IBajtpik3;

namespace Bajtpik.Bajtpik3;

public class GlobalData : IGlobalData
{
    private readonly Dictionary<int, string> _author;
    private readonly Dictionary<int, string> _boardgame;
    private readonly Dictionary<int, string> _book;
    private readonly Dictionary<int, string> _newspaper;


    public GlobalData()
    {
        _newspaper = new Dictionary<int, string>();
        _author = new Dictionary<int, string>();
        _book = new Dictionary<int, string>();
        _boardgame = new Dictionary<int, string>();
    }

    public Dictionary<int, string> GetBookDictionary()
    {
        return _book;
    }

    public Dictionary<int, string> GetAuthorDictionary()
    {
        return _author;
    }

    public Dictionary<int, string> GetBoardGameDictionary()
    {
        return _boardgame;
    }

    public int GetAuthorKey(string name, string surname, int? birthYear = null)
    {
        foreach (var authorEntry in _author)
        {
            var authorDetails = authorEntry.Value.Split(',');

            // Using the StringSplitOptions.RemoveEmptyEntries to avoid empty entries
            var nameParts = authorDetails[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2) continue;

            var firstName = nameParts[0].Trim();
            var lastName = nameParts[1].Trim();

            if (!firstName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                !lastName.Equals(surname, StringComparison.OrdinalIgnoreCase)) continue;

            if (birthYear.HasValue)
            {
                if (int.TryParse(authorDetails[1].Trim(), out int authorBirthYear) &&
                    birthYear.Value == authorBirthYear)
                {
                    return authorEntry.Key;
                }
            }
            else
            {
                return authorEntry.Key;
            }
        }

        return -1;
    }



    public void PrintAllNewspapers()
    {
        foreach (var kvp in _newspaper) Console.WriteLine("{0}", kvp.Value);
    }

    public void PrintAllAuthors()
    {
        foreach (var kvp in _author) Console.WriteLine("{0}", kvp.Value);
    }

    public void PrintAllBooks()
    {
        foreach (var kvp in _book) Console.WriteLine("{0}", kvp.Value);
    }

    public void PrintAllBoardgames()
    {
        foreach (var kvp in _boardgame) Console.WriteLine("{0}", kvp.Value);
    }

    public void AddAuthor(string name, string surname, int birthYear, string nickname)
    {
        var value = nickname == "" ? $"{name} {surname}, {birthYear}" : $"{name} {surname}, {birthYear}, {nickname}";
        var key = (name + surname + birthYear).GetHashCode();
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
        var authorNames = new List<string>();

        foreach (var authorKey in authorKeys)
            if (_author.TryGetValue(authorKey, out var authorInfo))
            {
                var authorDetails = authorInfo.Split(',');
                var authorName = authorDetails[0].Trim();
                authorNames.Add(authorName);
            }

        if (authorNames.Count <= 0) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"{title}, {authors}, {year}, {pageCount}";
        var key = (title + string.Join("", authorNames) + year + pageCount).GetHashCode();
        _book.Add(key, value);
    }

    public void AddBoardgame(string title, int minPlayers, int maxPlayers, int difficulty, List<int> authorKeys)
    {
        var authorNames = new List<string>();

        foreach (var authorKey in authorKeys)
            if (_author.TryGetValue(authorKey, out var authorInfo))
            {
                var authorDetails = authorInfo.Split(',');
                var authorName = authorDetails[0].Trim();
                if (authorName.StartsWith("[")) authorName = authorName.Substring(1);

                authorNames.Add(authorName);
            }

        if (authorNames.Count <= 0) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"\"{title}\", {minPlayers}, {maxPlayers}, {difficulty}, {authors}";
        var key = (title + minPlayers + maxPlayers + difficulty + string.Join("", authorNames)).GetHashCode();
        _boardgame.Add(key, value);
    }
}