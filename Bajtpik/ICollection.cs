namespace Bajtpik;

public interface ICollection<T>
{
    void Add(T? item);
    void Delete(T? item);
    IEnumerable<T?> ForwardIterator();
    IEnumerable<T?> ReverseIterator();
}