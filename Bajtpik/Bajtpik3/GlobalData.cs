using Bajtpik.Bajtpik3.IBajtpik3;

namespace Bajtpik.Bajtpik3;

public class GlobalData : IGlobalData
{
    private readonly Dictionary<int, string> _authors;
    private readonly Dictionary<int, string> _boardgames;
    private readonly Dictionary<int, string> _books;
    private readonly Dictionary<int, string> _newspapers;


    public GlobalData()
    {
        _newspapers = new Dictionary<int, string>();
        _authors = new Dictionary<int, string>();
        _books = new Dictionary<int, string>();
        _boardgames = new Dictionary<int, string>();
    }

    public Dictionary<int, string> GetBookDictionary()
    {
        return _books;
    }

    public Dictionary<int, string> GetAuthorDictionary()
    {
        return _authors;
    }

    public Dictionary<int, string> GetBoardGameDictionary()
    {
        return _boardgames;
    }

    public int GetAuthorKey(string name, string surname, int? birthYear = null)
    {
        foreach (var authorEntry in _authors)
        {
            var authorDetails = authorEntry.Value.Split(',');

            var nameParts = authorDetails[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2) continue;

            var firstName = nameParts[0].Trim();
            var lastName = nameParts[1].Trim();

            if (!firstName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                !lastName.Equals(surname, StringComparison.OrdinalIgnoreCase)) continue;

            if (birthYear.HasValue)
            {
                if (int.TryParse(authorDetails[1].Trim(), out var authorBirthYear) &&
                    birthYear.Value == authorBirthYear)
                    return authorEntry.Key;
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
        PrintItems(_newspapers);
    }

    public void PrintAllAuthors()
    {
        PrintItems(_authors);
    }

    public void PrintAllBooks()
    {
        PrintItems(_books);
    }

    public void PrintAllBoardgames()
    {
        PrintItems(_boardgames);
    }

    private static void PrintItems(Dictionary<int, string> items)
    {
        foreach (var kvp in items)
            Console.WriteLine(kvp.Value);
    }

    public void AddAuthor(string name, string surname, int birthYear, string nickname)
    {
        var value = nickname == "" ? $"{name} {surname}, {birthYear}" : $"{name} {surname}, {birthYear}, {nickname}";
        var key = (name + surname + birthYear).GetHashCode();
        _authors.Add(key, value);
    }

    public void AddNewspaper(string title, int year, int pageCount)
    {
        var value = $"{title}, {year}, {pageCount} ";
        var key = (title + year + pageCount).GetHashCode();
        _newspapers.Add(key, value);
    }

    public void AddBook(string title, IEnumerable<int> authorKeys, int year, int pageCount)
    {
        var authorNames = GetAuthorNames(authorKeys);
        if (!authorNames.Any()) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"{title}, {authors}, {year}, {pageCount}";
        var key = (title + string.Join("", authorNames) + year + pageCount).GetHashCode();
        _books.Add(key, value);
    }

    public void AddBoardgame(string title, int minPlayers, int maxPlayers, int difficulty, IEnumerable<int> authorKeys)
    {
        var authorNames = GetAuthorNames(authorKeys);
        if (!authorNames.Any()) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"\"{title}\", {minPlayers}, {maxPlayers}, {difficulty}, {authors}";
        var key = (title + minPlayers + maxPlayers + difficulty + string.Join("", authorNames)).GetHashCode();
        _boardgames.Add(key, value);
    }

    private List<string> GetAuthorNames(IEnumerable<int> authorKeys)
    {
        return authorKeys
            .Select(authorKey =>
                _authors.TryGetValue(authorKey, out var authorInfo) ? authorInfo.Split(',')[0].Trim() : null)
            .Where(authorName => !string.IsNullOrEmpty(authorName))
            .ToList()!;
    }
}