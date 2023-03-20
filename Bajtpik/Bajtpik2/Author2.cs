namespace Bajtpik.Bajtpik2;

public class Author2 : IAuthor2
{
    public static int Count { get; set; }

    private int _id = 1;

    private readonly Dictionary<string, string> _authorDict = new();

    public void AddAuthor(string name, string surname, int birthYear, string? nickname)
    {
        _authorDict.Add(_id + ".name[0]", name);
        _authorDict.Add(_id + ".surname[0]", surname);
        _authorDict.Add(_id + ".birthYear[0]", birthYear.ToString());
        _authorDict.Add(_id + ".nickname[0]", nickname ?? string.Empty);
        _id++;
    }

    public string GetAuthor(int id)
    {
        return _authorDict[id + ".name[0]"] + " , " + _authorDict[id + ".surname[0]"] + " , " +
               _authorDict[id + ".birthYear[0]"] + " , " + _authorDict[id + ".nickname[0]"];
    }


    public void PrintAuthor2(int id)
    {
        Console.WriteLine(_authorDict[id + ".name[0]"] + " , " + _authorDict[id + ".surname[0]"] + " , " +
                          _authorDict[id + ".birthYear[0]"] + " , " + _authorDict[id + ".nickname[0]"]);
    }
}