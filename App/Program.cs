using Trees;

class Program
{
    public static void Main()
    {
        int[] x = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17};
        var y = Utils.Build(x,0,x.Length);
        var w = y.DFS();
        System.Console.WriteLine(string.Join(" ", w));
        var z = y.SubtreesSelect(Transform).DFS();
        System.Console.WriteLine(string.Join(" ", z));
    }
    public static string Transform(int x){
        return x switch{
            1 => "Uno",
            2 => "Dos",
            3 => "Tres",
            4 => "Cuatro",
            5 => "Cinco",
            6 => "Seis",
            7 => "Siete",
            8 => "Ocho",
            9 => "Nueve",
            10 => "Diez",
            11 => "Once",
            12 => "Doce",
            13 => "Trece",
            14 => "Catorce",
            15 => "Quince",
            16 => "Dieciseis",
            17 => "Dieciete",
            _ => "No se que es"
        };
    }
}