using Bajtpik.Bajtpik2;

namespace Bajtpik;

public class Newspaper2 : INewspaper2
{
    private int _id = 1;

    private readonly Dictionary<string, string> _newspaperDict = new();

    public void AddNewspaper(string title, int year, int? pageCount)
    {
        _newspaperDict.Add(_id + ".title[0]", title);
        _newspaperDict.Add(_id + ".year[0]", year.ToString());
        _newspaperDict.Add(_id + ".pageCount[0]", pageCount.ToString() ?? string.Empty);
        _id++;
    }

    public void PrintNewspaper2(int id)
    {
        Console.WriteLine(_newspaperDict[id + ".title[0]"] + " , " + _newspaperDict[id + ".year[0]"] + " , " +
                          _newspaperDict[id + ".pageCount[0]"]);
    }
}