namespace Bajtpik;

public class Boardgame : IBoardgame
{
    public List<Author> Authors;
    public int Difficulty;
    public int MaxPlayers;
    public int MinPlayers;
    public string Title;

    public void PrintBoardgame()
    {
        Console.WriteLine(
            $"\"{Title}\", {MinPlayers}, {MaxPlayers}, {Difficulty}, {(Authors.Count > 1 ? "[" : "")}{string.Join(", ", Authors.Select(a => $"{a.Name} {a.Surname}"))}{(Authors.Count > 1 ? "]" : "")}");
    }

    public void PrintBoardgameAuthorBornAfter1970()
    {
        if (Authors.Any(author => author.BirthYear > 1970)) PrintBoardgame();
    }
}