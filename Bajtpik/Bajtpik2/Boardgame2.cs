using Bajtpik.Bajtpik2.iBajtpik2;

namespace Bajtpik.Bajtpik2;

public class Boardgame2 : IBoardgame2
{
    private int _id = 1;
    private readonly Dictionary<string, string> _boardGameDict = new();

    public string this[string key]
    {
        get => _boardGameDict.TryGetValue(key, out var value) ? value : $"Invalid key: {key}";
        set => _boardGameDict[key] = value;
    }

    public int AddBoardgame(string title, int minPlayers = 0, int maxPlayers = 0, int difficulty = 0,
        List<int> authorId = null)
    {
        _boardGameDict.Add(_id + ".title[0]", title);
        _boardGameDict.Add(_id + ".authorId.count", authorId.Count.ToString());
        var authorIdCount = 0;
        foreach (var authorIdItem in authorId)
        {
            _boardGameDict.Add(_id + $".authorId[{authorIdCount}]", authorIdItem.ToString());
            ++authorIdCount;
        }

        _boardGameDict.Add(_id + ".minPlayers[0]", minPlayers.ToString());
        _boardGameDict.Add(_id + ".maxPlayers[0]", maxPlayers.ToString());
        _boardGameDict.Add(_id + ".difficulty[0]", difficulty.ToString());
        _boardGameDict["boardgame_count"] = _id.ToString();
        return _id++;
    }

    public void PrintBoardgame2(int id, Author2 author2)
    {
        var authorIdStr = "";
        for (var i = 0; i < Convert.ToInt32(_boardGameDict[id + ".authorId.count"]); ++i)
        {
            var authorId = Convert.ToInt32(_boardGameDict[id + $".authorId[{i}]"]);
            authorIdStr += $" {i + 1}. " + author2[authorId + ".name[0]"] + " " + author2[authorId + ".surname[0]"];
        }

        authorIdStr = authorIdStr.Remove(authorIdStr.Length - 1);
        Console.WriteLine(
            $"{_boardGameDict[id + ".title[0]"]},{_boardGameDict[id + ".minPlayers[0]"]}, {_boardGameDict[id + ".maxPlayers[0]"]}, {_boardGameDict[id + ".difficulty[0]"]}, {authorIdStr}");
    }
}