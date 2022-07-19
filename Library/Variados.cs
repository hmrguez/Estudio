namespace Library;

public static class Variados
{
    public static string LCS(string a, string b)
    {
        string mejor = "";
        int ultimo = b.Length;
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < ultimo; j++)
            {
                if (a[i] == b[j])
                {
                    var actual = a[i] + LCS(a.Substring(i + 1), b.Substring(j + 1));
                    if (actual.Length > mejor.Length) mejor = actual;
                    ultimo = j;
                }
            }
        }
        return mejor;
    }

    public static int MultOptimaDeMatrices(int[] matrices){
        return MultOptimaDeMatrices(matrices,0,matrices.Length-2);
    }
    public static int MultOptimaDeMatrices(int[] matrices, int ini, int fin){
        if(ini==fin) return 0;
        int mejor = int.MaxValue;
        for (int i = 1 ; i < fin-ini+1; i++)
        {
            int costoIzq = MultOptimaDeMatrices(matrices,ini,ini+i-1);
            int costoDer = MultOptimaDeMatrices(matrices,ini + i,fin);
            int costoUltOperacion = matrices[ini]*matrices[ini+i] * matrices[fin+1];
            int costoGlobal = costoIzq +costoDer + costoUltOperacion;
            if(costoGlobal<mejor) mejor = costoGlobal;
        }
        return mejor;
    }
}