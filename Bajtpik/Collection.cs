namespace Bajtpik;

public class DoublyLinkedList<T> : ICollection<T>
{
    private Node? _head;
    private Node? _tail;

    public void Add(T? item)
    {
        var newNode = new Node { Data = item };

        if (_tail == null)
        {
            _head = _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail;
            _tail.Next = newNode;
            _tail = newNode;
        }
    }

    public void Delete(T? item)
    {
        var current = _head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, item))
            {
                if (current.Prev != null)
                    current.Prev.Next = current.Next;
                else
                    _head = current.Next;

                if (current.Next != null)
                    current.Next.Prev = current.Prev;
                else
                    _tail = current.Prev;

                return;
            }

            current = current.Next;
        }
    }

    public IEnumerable<T?> ForwardIterator()
    {
        var current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    public IEnumerable<T?> ReverseIterator()
    {
        var current = _tail;
        while (current != null)
        {
            yield return current.Data;
            current = current.Prev;
        }
    }

    private class Node
    {
        public T? Data { get; init; }
        public Node? Next { get; set; }
        public Node? Prev { get; set; }
    }
}

public class Vector<T> : ICollection<T>
{
    private readonly List<T?> _items = new();

    public void Add(T? item)
    {
        _items.Add(item);
    }

    public void Delete(T? item)
    {
        _items.Remove(item);
    }

    public IEnumerable<T?> ForwardIterator()
    {
        return _items;
    }

    public IEnumerable<T?> ReverseIterator()
    {
        for (var i = _items.Count - 1; i >= 0; i--) yield return _items[i];
    }
}

public static class CollectionAlgorithms
{
    public static T? Find<T>(this ICollection<T?> collection, Func<T, bool> predicate, bool searchFromEnd = false)
    {
        using var iterator =
            (searchFromEnd ? collection.ReverseIterator() : collection.ForwardIterator()).GetEnumerator();
        while (iterator.MoveNext())
            if (predicate(iterator.Current ??
                          throw new ArgumentNullException(nameof(iterator.Current), "Current is null")))
                return iterator.Current;
        return default;
    }

    public static void Print<T>(this ICollection<T?> collection, Func<T, bool> predicate, Action<T> action,
        bool searchFromEnd = false)
    {
        using var iterator =
            (searchFromEnd ? collection.ReverseIterator() : collection.ForwardIterator()).GetEnumerator();
        while (iterator.MoveNext())
            if (predicate(iterator.Current ??
                          throw new ArgumentNullException(nameof(iterator.Current), "Current is null")))
                action(iterator.Current);
    }
}