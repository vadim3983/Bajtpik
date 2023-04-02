namespace Bajtpik;

public interface ICollection<T>
{
    void Add(T? item);
    void Delete(T? item);
    IEnumerator<T?> ForwardIterator();
    IEnumerator<T?> ReverseIterator();
}