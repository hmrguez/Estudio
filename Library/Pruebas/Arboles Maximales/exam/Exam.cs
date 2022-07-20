namespace MatCom.Exam
{
    public static class Exam
    {
        public static IEnumerable<ITree<T>> MaximalSubtreesWhere<T>(
            ITree<T> tree, Predicate<T> predicate)
        {
            List<ITree<T>> anadidos = new();

            foreach (var item in tree.DFS())
            {
                if(anadidos.Exists(x=>x.Children.Contains(item))){
                    anadidos.Add(item);
                    continue;
                }

                if(item.DFS().All(x=>predicate(x.Value))){
                    yield return item;
                    anadidos.Add(item);
                }
            }
        }
        public static IEnumerable<ITree<T>> DFS<T>(this ITree<T> x){
            yield return x;
            foreach (var item in x.Children)
            {
                foreach (var item2 in item.DFS())
                {
                    yield return item2;
                }
            }
        }
        
        public static string Nombre => "Hector Miguel Rodriguez Sosa";
        public static string Grupo => "C211";
    }
}
