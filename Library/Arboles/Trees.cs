namespace Trees;

public class Arbol<T>
{
    public Arbol(T value, params Arbol<T>[] children)
    {
        Value = value;
        Children = children;
    }

    public T Value { get; set; }
    public IEnumerable<Arbol<T>> Children { get; set; }
    public int Height
    {
        get
        {
            int o = 0;
            foreach (var item in Children)
                if (item != null)
                    o = Math.Max(item.Height, o);
            return o + 1;
        }
    }

    public int Diameter
    {
        get
        {
            int d = 0, h1 = 0, h2 = 0;
            foreach (var item in Children)
            {
                if (item != null)
                {
                    d = Math.Max(d, item.Diameter);

                    if (item.Height > h1)
                    {
                        h2 = h1;
                        h1 = item.Height;
                    }
                    else if (item.Height > h2)
                    {
                        h2 = item.Height;
                    }
                }
            }
            return Math.Max(d, h1 + h2 + 1);
        }
    }
    public IEnumerable<T> DFS()
    {
        yield return Value;
        foreach (var item in Children)
        {
            if (item != null)
                foreach (var item2 in item.DFS())
                    yield return item2;
        }
    }
    public IEnumerable<T> BFS()
    {
        Queue<Arbol<T>> cola = new();
        cola.Enqueue(this);
        while (cola.Any())
        {
            yield return cola.Peek().Value;
            foreach (var item in cola.Peek().Children)
                if (item != null)
                    cola.Enqueue(item);
            cola.Dequeue();
        }
    }

}

public static class Utils
{
    public static Arbol<T> Build<T>(T[] array, int start, int end)
    {
        if (start == end) return null!;
        int mid = (start + end) / 2;
        var x = Build(array, start, mid);
        var y = Build(array, mid + 1, end);
        return new Arbol<T>(array[mid], x, y);
    }
}