namespace Bajtpik;

public interface IPublication
{
    string Title { get; set; }
    int Year { get; set; }
    int? PageCount { get; set; }
}