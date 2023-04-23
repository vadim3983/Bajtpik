namespace Bajtpik.Bajtpik2;

public class Author2 : IAuthor2
{
    public readonly Dictionary<string, string?> _authorDict = new();
    public int _id = 1;

    internal string? this[string key]
    {
        get => _authorDict.TryGetValue(key, out var value) ? value : $"Invalid key: {key}";
        set => _authorDict[key] = value;
    }

    public void PrintAuthor2(int id)
    {
        Console.WriteLine(
            $"{this[id + ".name[0]"]} {this[id + ".surname[0]"]}, {this[id + ".birthYear[0]"]} {this[id + ".nickname[0]"]}");
    }

    public void AddAuthor(string name, string surname, int birthYear, string? nickname)
    {
        this[_id + ".name[0]"] = name;
        this[_id + ".surname[0]"] = surname;
        this[_id + ".birthYear[0]"] = birthYear.ToString();
        this[_id + ".nickname[0]"] = nickname;
        _id++;
    }

    public int GetBirthYear()
    {
        return int.Parse(this[_id + ".birthYear[0]"]);
    }
}