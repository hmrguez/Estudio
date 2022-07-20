using Trees;

class Program
{
    public static void Main()
    {
        int[] x = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17};
        var y = Utils.Build(x,0,x.Length);
        System.Console.WriteLine(string.Join(" ", y.DFS()));
        System.Console.WriteLine(string.Join(" ", y.BFS()));
        System.Console.WriteLine(y.Height);
        System.Console.WriteLine(y.Diameter);
    }
}