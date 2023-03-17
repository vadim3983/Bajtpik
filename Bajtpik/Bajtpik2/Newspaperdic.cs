using Bajtpik.Bajtpik2;

namespace Bajtpik;

public class Newspaper2: INewspaper2
{
    private int _id;
    
    private readonly Dictionary<string, string> _newspaperDict = new Dictionary<string, string>();
    
    
    public Newspaper2(string title, int year, int? pageCount)
    {
        _id = _newspaperDict.Count;
        _newspaperDict.Add(_id + ".title[0]", title);
        _newspaperDict.Add(_id + ".year[0]", year.ToString());
        _newspaperDict.Add(_id + ".pageCount[0]", pageCount.ToString() ?? string.Empty);
    }
    
    //create a method to add new newspapers
    
    public void AddNewspaper(string title, int year, int? pageCount)
    {
        _id = _newspaperDict.Count;
        _newspaperDict.Add(_id + ".title[0]", title);
        _newspaperDict.Add(_id + ".year[0]", year.ToString());
        _newspaperDict.Add(_id + ".pageCount[0]", pageCount.ToString() ?? string.Empty);
    }
    
    public void PrintNewspaper2()
    {
        Console.WriteLine(_newspaperDict[_id + ".title[0]"] + " " + _newspaperDict[_id + ".year[0]"] + " " + _newspaperDict[_id + ".pageCount[0]"]);
    }
}
/* create an object of Newspaper2 class and
 add these newspapers 
 Newspaper - title, year, pageCount
1. International Journal of Human-Computer Studies, MaxValue, 300
2. Nature, 1869, 200
3. National Geographic, 2001, 106
4. Pixel. 2015, 115
 */





