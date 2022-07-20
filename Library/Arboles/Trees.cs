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
}

public static class Utils
{
    public static Arbol<T> Build<T>(T[] array, int start, int end)
    {
        if (start == end) return null!;
        int mid = (start+end)/2;
        var x = Build(array,start,mid);
        var y = Build(array,mid+1,end);
        return new Arbol<T>(array[mid],x,y);
    }
}