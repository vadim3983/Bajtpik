using Bajtpik.Bajtpik2;

namespace Bajtpik.Adapters;

public class NewspaperAdapter : INewspaper
{
    private readonly INewspaper2 _newspaper2;
    private int _id = 1;

    public NewspaperAdapter(INewspaper2 newspaper2)
    {
        _newspaper2 = newspaper2;
    }

    public void PrintNewspaper()
    {
        _newspaper2.PrintNewspaper2(_id);
        _id++;
    }
}