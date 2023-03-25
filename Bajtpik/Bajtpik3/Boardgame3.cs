namespace Bajtpik.Bajtpik3;

public class Boardgame3 : IBoardgame3
{
    private readonly Dictionary<int, string> _boardgame = new();
    private readonly int _difficulty;
    private readonly int _maxPlayers;
    private readonly int _minPlayers;
    private readonly string _title;

    public Boardgame3(string title, int minPlayers, int maxPlayers, int difficulty)
    {
        _title = title;
        _minPlayers = minPlayers;
        _maxPlayers = maxPlayers;
        _difficulty = difficulty;

        _boardgame[title.GetHashCode()] = title;
        _boardgame[minPlayers.ToString().GetHashCode()] = minPlayers.ToString();
        _boardgame[maxPlayers.ToString().GetHashCode()] = maxPlayers.ToString();
        _boardgame[difficulty.ToString().GetHashCode()] = difficulty.ToString();
    }

    public void PrintBoardgame(List<Author3> authors)
    {
        var title = _boardgame[_title.GetHashCode()];
        var minPlayers = _boardgame[_minPlayers.ToString().GetHashCode()];
        var maxPlayers = _boardgame[_maxPlayers.ToString().GetHashCode()];
        var difficulty = _boardgame[_difficulty.ToString().GetHashCode()];

        var authorNames = string.Join(", ", authors.Select(author => $"{author.GetName()} {author.GetSurname()}"));
        if (authors.Count > 1) authorNames = $"[{authorNames}]";

        var output = $"\"{title}\", {minPlayers}, {maxPlayers}, {difficulty}, {authorNames}";

        Console.WriteLine(output);
    }
}