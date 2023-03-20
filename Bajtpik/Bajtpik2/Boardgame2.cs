namespace Bajtpik.Bajtpik2;

public class Boardgame2 : IBoardgame2
{
    /*
         6. Each object is represented by unique integer identifer. All data is kept in one map string->string. 
    Representation is a bit similar to 1. but data is accesed as string in that map. Additionally "<fieldname_count>" 
    like in representation 6. Data is accessed like "<id>.<fieldname>[idx]". So to eg. access title of book 3 use 
    string "3.title[0]" and to access second author of that book - "3.authors[1]". To access number of authors of that 
    book use "3.authors_count" 
    BoardGame - title, minPlayers, maxPlayers, difficulty, authors
1. “Scythe”, 1, 5, 7, [Jamey Stegmaier, Jakub Różalski]
2. “Catan”, 3, 4, 6, Klaus Teuber
3. “Scrabble”, 2, 4, 5, [James Brunot, Alfred Butts]
4. “Twilight Imperium”, 3, 8, 9, Christian T. Petersen
     */

    public static int Count { get; set; }

    private int _id = 1;

    private readonly Dictionary<string, string> _boardGameDict = new();

    public void AddBoardgame(string title, int minPlayers, int maxPlayers, int difficulty, string authors)
    {
        _boardGameDict.Add(_id + ".title[0]", title);
        _boardGameDict.Add(_id + ".minPlayers[0]", minPlayers.ToString());
        _boardGameDict.Add(_id + ".maxPlayers[0]", maxPlayers.ToString());
        _boardGameDict.Add(_id + ".difficulty[0]", difficulty.ToString());
        _boardGameDict.Add(_id + ".authors_count", authors.Split(",").Length.ToString());
        _boardGameDict.Add(_id + ".authors[0]", authors.Split(",")[0]);
        _boardGameDict.Add(_id + ".authors[1]", authors.Split(",")[1]);
        _id++;
    }

    public void PrintBoardgame2(int id)
    {
        Console.WriteLine(_boardGameDict[id + ".title[0]"] + " , " + _boardGameDict[id + ".minPlayers[0]"] + " , " +
                          _boardGameDict[id + ".maxPlayers[0]"] + " , " + _boardGameDict[id + ".difficulty[0]"] +
                          " , " +
                          _boardGameDict[id + ".authors_count"] + " , " +
                          _boardGameDict[id + ".authors[0]"] + " , " +
                          _boardGameDict[id + ".authors[1]"]);
    }
}