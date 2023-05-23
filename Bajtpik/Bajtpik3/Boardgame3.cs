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

    public Boardgame3(GlobalData globalData, string title, int minPlayers, int maxPlayers, int difficulty,
        List<Author?> authors)
    {
        _boardgame = globalData;
        Title = title;
        MinPlayers = minPlayers;
        MaxPlayers = maxPlayers;
        Difficulty = difficulty;
        Authors = authors;

        _boardgame.AddBoardgame(this);
        foreach (var author in authors.Where(author => author != null))
            _boardgame.AddAuthor(author?.Name!, author.Surname!, author.BirthYear, author.Nickname!);
    }

    public string Title { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int Difficulty { get; set; }
    public List<Author?> Authors { get; set; } = new();
}