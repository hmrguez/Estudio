using Trees;

class Program
{
    public static void Main()
    {
        int[] x = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17};
        var y = Utils.Build(x,0,x.Length);
        System.Console.WriteLine(Utils.LCA(4,3,y).Value);
    }
}