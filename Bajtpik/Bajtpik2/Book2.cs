namespace Bajtpik.Bajtpik2;

public class Book2 : IBook2
{
    private readonly Dictionary<string, string> _bookDict = new();
    private int _id = 1;

    public string this[string key]
    {
        get => _bookDict.TryGetValue(key, out var value) ? value : $"Invalid key: {key}";
        set => _bookDict[key] = value;
    }

    public void PrintBook2(int id, Author2 author2)
    {
        var authorNames = new List<string>();
        for (var i = 0; i < Convert.ToInt32(_bookDict[id + ".authorId.count"]); ++i)
        {
            var authorId = Convert.ToInt32(_bookDict[id + $".authorId[{i}]"]);
            authorNames.Add(author2[authorId + ".name[0]"] + " " + author2[authorId + ".surname[0]"]);
        }

        var authorStr = authorNames.Count > 1 ? "[" + string.Join(", ", authorNames) + "]" : authorNames[0];
        Console.WriteLine(
            $"{_bookDict[id + ".title[0]"]}, {authorStr}, {_bookDict[id + ".releaseYear[0]"]}, {_bookDict[id + ".pages[0]"]}");
    }

    public int AddBook(string title, List<int> authorId, int releaseYear = 0, int pages = 0)
    {
        _bookDict.Add(_id + ".title[0]", title);
        _bookDict.Add(_id + ".authorId.count", authorId.Count.ToString());
        var authorIdCount = 0;
        foreach (var authorIdItem in authorId)
        {
            _bookDict.Add(_id + $".authorId[{authorIdCount}]", authorIdItem.ToString());
            ++authorIdCount;
        }

        _bookDict.Add(_id + ".releaseYear[0]", releaseYear.ToString());
        _bookDict.Add(_id + ".pages[0]", pages.ToString());
        _bookDict["book_count"] = _id.ToString();
        return _id++;
    }
}