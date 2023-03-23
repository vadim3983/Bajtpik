namespace Bajtpik.Bajtpik2;

public class Author2 : IAuthor2
{
    public int _id = 1;

    public readonly Dictionary<string, string?> _authorDict = new();

    internal string? this[string key]
    {
        get => _authorDict.TryGetValue(key, out var value) ? value : $"Invalid key: {key}";
        set => _authorDict[key] = value;
    }

    public int AddAuthor(string name = "undef", string surname = "undef", int birthYear = 0, string? nickname = "undef")
    {
        _authorDict.Add(_id + ".name[0]", name);
        _authorDict.Add(_id + ".surname[0]", surname);
        _authorDict.Add(_id + ".birthYear[0]", birthYear.ToString());
        _authorDict.Add(_id + ".nickname[0]", nickname);
        _authorDict["authors_count"] = _id.ToString();
        return _id++;
    }


    public void PrintAuthor2(int id)
    {
        Console.WriteLine(
            $"{this[id + ".name[0]"]} {this[id + ".surname[0]"]}, {this[id + ".birthYear[0]"]} {this[id + ".nickname[0]"]}");
    }
}