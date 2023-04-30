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

public class Heap<T> : ICollection<T>
{
    private readonly Comparison<T?> _comparator;
    private readonly List<T?> _items = new();

    public Heap(Comparison<T?> comparator)
    {
        _comparator = comparator;
    }

    public void Add(T? item)
    {
        _items.Add(item);
        var i = _items.Count - 1;
        while (i > 0 && _comparator(_items[i], _items[(i - 1) / 2]) < 0)
        {
            Swap(i, (i - 1) / 2);
            i = (i - 1) / 2;
        }
    }

    public void Delete(T? item)
    {
        var index = _items.IndexOf(item);
        if (index == -1) return;

        _items[index] = _items[_items.Count - 1];
        _items.RemoveAt(_items.Count - 1);

        var i = index;
        while (true)
        {
            var left = 2 * i + 1;
            var right = 2 * i + 2;
            var smallest = i;

            if (left < _items.Count && _comparator(_items[left], _items[smallest]) < 0) smallest = left;
            if (right < _items.Count && _comparator(_items[right], _items[smallest]) < 0) smallest = right;

            if (smallest == i) break;

            Swap(i, smallest);
            i = smallest;
        }
    }

    public IEnumerable<T?> ForwardIterator()
    {
        return _items;
    }

    public IEnumerable<T?> ReverseIterator()
    {
        for (var i = _items.Count - 1; i >= 0; i--) yield return _items[i];
    }

    private void Swap(int i, int j)
    {
        (_items[i], _items[j]) = (_items[j], _items[i]);
    }
}

public static class CollectionAlgorithms
{
    public static T? Find<T>(IEnumerator<T> iterator, Func<T, bool> predicate)
    {
        while (iterator.MoveNext())
            if (predicate(iterator.Current))
                return iterator.Current;
        return default;
    }

    public static void Print<T>(IEnumerator<T> iterator, Func<T, bool> predicate, Action<T> action)
    {
        while (iterator.MoveNext())
            if (predicate(iterator.Current))
                action(iterator.Current);
    }

    public static void ForEach<T>(IEnumerator<T> iterator, Action<T> action)
    {
        while (iterator.MoveNext()) action(iterator.Current);
    }

    public static int CountIf<T>(IEnumerator<T> iterator, Func<T, bool> predicate)
    {
        var count = 0;
        while (iterator.MoveNext())
            if (predicate(iterator.Current))
                count++;
        return count;
    }
}