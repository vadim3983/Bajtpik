using Bajtpik.Bajtpik2;

namespace Bajtpik.Adapters;

public class BoardGamesAdapter : IBoardgame
{
    private readonly Boardgame2 _boardgame2;
    private readonly Author2 _author2;
    private int _id = 1;

    public BoardGamesAdapter(Boardgame2 boardgame2, Author2 author2)
    {
        _boardgame2 = boardgame2;
        _author2 = author2;
    }

    public void PrintBoardgame()
    {
        _boardgame2.PrintBoardgame2(_id, _author2);
        _id++;
    }
}