namespace Bajtpik.Bajtpik3;

public class Boardgame3
{
    private readonly GlobalData _boardgame;

    public Boardgame3(GlobalData globalData, string title, int minPlayers, int maxPlayers, int difficulty,
        List<int> authorKeys)
    {
        _boardgame = globalData;
        _boardgame.AddBoardgame(title, minPlayers, maxPlayers, difficulty, authorKeys);
    }
}