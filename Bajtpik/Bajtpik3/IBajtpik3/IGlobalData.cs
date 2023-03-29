namespace Bajtpik.Bajtpik3.IBajtpik3;

public interface IGlobalData
{
    void PrintAllNewspapers();
    void PrintAllAuthors();
    void PrintAllBooks();
    void PrintAllBoardgames();

    public Dictionary<int, string> GetBookDictionary();

    public Dictionary<int, string> GetAuthorDictionary();

    public Dictionary<int, string> GetBoardGameDictionary();

    public int GetAuthorKey(string name, string surname, int? birthYear = null);
}