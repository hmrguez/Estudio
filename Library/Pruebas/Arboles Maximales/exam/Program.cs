using MatCom.Exam;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // Para que no se olvide de rellenar estas propiedades
        Console.WriteLine(Exam.Nombre + " " + Exam.Grupo);

        // Puede probar su código aquí
        // Exam.MaximalSubtreesWhere(..., ...)

        var a = new Tre<int>(3);
        var b = new Tre<int>(6);
        var c = new Tre<int>(2,a,b);
        var d = new Tre<int>(0);
        var e = new Tre<int>(8);
        var q = new Tre<int>(24);
        var f = new Tre<int>(4,d,e);
        var g = new Tre<int>(5,c,f);
        var h = new Tre<int>(9);
        var i = new Tre<int>(10);
        var j = new Tre<int>(11,h,i);
        var k = new Tre<int>(12,j,g,q);
        var l = new Tre<int>(14);
        var m = new Tre<int>(16);
        var n = new Tre<int>(18);
        var o = new Tre<int>(20,l,m,n);
        var r = new Tre<int>(50);
        var s = new Tre<int>(49,k);
        var t = new Tre<int>(51,r, s, o);
        var u = new Tre<int>(100);
        var v = new Tre<int>(102);
        var w = new Tre<int>(104);
        var z = new Tre<int>(108,u,v,w);
        var aa = new Tre<int>(106,z,t);
        System.Console.WriteLine();
        System.Console.WriteLine(string.Join(" ", Exam.MaximalSubtreesWhere(aa,y=> y+2>10).Select(item=>item.Value)));

    }
}
class Tre<T>: ITree<T>{
    public T Value { get; set; }
    public IEnumerable<ITree<T>> Children { get; set; }
    public Tre(T x, params ITree<T>[] y){
        Value = x;
        Children = y;
    }
}