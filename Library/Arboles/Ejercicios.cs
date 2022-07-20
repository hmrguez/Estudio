namespace Trees;

public static class Ejercicios
{
    public static IEnumerable<Arbol<T>> SubtreesWhere<T>(this Arbol<T> tree, Predicate<T> pred) => tree.DFSTree().Where(x => pred(x.Value));
    public static IEnumerable<Arbol<T>> MaximalSubtreesWhere<T>(Arbol<T> tree, Predicate<T> predicate)
    {
        var mscs = tree.Children.Where(x => x != null).Select(ch => (ch, MaximalSubtreesWhere(ch, predicate)));
        if (predicate(tree.Value) && mscs.All(msc => msc.Item2.Take(1).Contains(msc.Item1))) yield return tree;
        else foreach (var item in mscs) foreach (var item2 in item.Item2) yield return item2;
    }
    public static IEnumerable<Arbol<T>> InOrder<T>(this Arbol<T> tree)
    {
        var x = tree.Children.Take(2).ToArray();
        if (x[0] != null) foreach (var item in x[0].InOrder()) yield return item;
        yield return tree;
        if (x[1] != null) foreach (var item in x[1].InOrder()) yield return item;
    }
    public static Arbol<C> SubtreesSelect<T,C>(this Arbol<T> tree, Func<T, C> func) => new Arbol<C> (func(tree.Value),tree.Children.Where(x=>x!=null).Select(x=>SubtreesSelect(x,func)));
}