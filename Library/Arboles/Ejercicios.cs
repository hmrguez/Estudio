namespace Trees;

public static class Ejercicios
{
    public static IEnumerable<Arbol<T>> SubtreeWhere<T>(this Arbol<T> tree, Predicate<T> pred) => tree.DFSTree().Where(x => pred(x.Value));
    public static IEnumerable<Arbol<T>> MaximalSubtreesWhere<T>(Arbol<T> tree, Predicate<T> predicate)
    {
        List<Arbol<T>> anadidos = new();

        foreach (var item in tree.DFSTree())
        {
            if (anadidos.Exists(x => x.Children.Contains(item)))
            {
                anadidos.Add(item);
                continue;
            }

            if (item.DFSTree().All(x => predicate(x.Value)))
            {
                yield return item;
                anadidos.Add(item);
            }
        }
    }
}