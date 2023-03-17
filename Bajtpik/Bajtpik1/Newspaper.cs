namespace Bajtpik;

public class Newspaper: IPublication, INewspaper
{
    public string Title { get; set; }
    public int Year { get; set; }
    public int? PageCount { get; set; }
    
    public void PrintNewspaper()
    {
        Console.WriteLine(Title+ " , " + Year + " , " + PageCount);
    }
}





