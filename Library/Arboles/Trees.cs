namespace Trees;

public class Arbol<T>
{
    public Arbol(T value, params Arbol<T>[] children)
    {
        Value = value;
        Children = children;
    }

    public Arbol(T value, IEnumerable<Arbol<T>> children)
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
            if (item != null)
                foreach (var item2 in item.DFS())
                    yield return item2;
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
    public IEnumerable<Arbol<T>> DFSTree()
    {
        yield return this;
        foreach (var item in Children)
            if (item != null)
                foreach (var item2 in item.DFSTree())
                    yield return item2;
    }
    public bool Contains(T x) => DFS().Contains(x);
    public bool Contains(Arbol<T> x) => DFSTree().Contains(x);
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
    public static Arbol<T> LCA<T>(Arbol<T> x1, Arbol<T> x2, Arbol<T> raiz)
    {
        foreach (var item in raiz.Children)
            if (item.Contains(x1) && item.Contains(x2))
                return LCA(x1, x2, item);
        return raiz;
    }
    public static Arbol<T> LCA<T>(T x1, T x2, Arbol<T> raiz)
    {
        foreach (var item in raiz.Children)
            if (item.Contains(x1) && item.Contains(x2))
                return LCA(x1, x2, item);
        return raiz;
    }
}