namespace Bajtpik.Bajtpik3;

public class Newspaper3
{
    private readonly GlobalData _newspaper;

    public Newspaper3(GlobalData newspaper, string title, int year, int pageCount)
    {
        _newspaper = newspaper;
        _newspaper.AddNewspaper(title, year, pageCount);
    }
}