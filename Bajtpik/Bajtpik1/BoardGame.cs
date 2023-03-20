namespace Bajtpik;

public class Boardgame : IBoardgame
{
    public string Title;
    public int MinPlayers;
    public int MaxPlayers;
    public int Difficulty;
    public List<Author> Authors;

    public void PrintBoardgame()
    {
        Console.WriteLine(Title + " , " + MinPlayers + " , " + MaxPlayers + " , " + Difficulty + " , " +
                          Authors.Select(x => x.Name + " " + x.Surname).Aggregate((x, y) => x + ", " + y));
    }
}