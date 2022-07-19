namespace Library;


///<summary>Son todas las maneras diferntes de escoger elemtos de un array, imaginate unas variaciones sin repeticion pero donde el orden no importa</summary>
public static class Combinations
{
    public static void Combinaciones(int n, int m)
    {
        Combinaciones(n, new int[m], 0, 0);
    }
    public static void Combinaciones(int n, int[] combinacion, int cuantas, int menorAPoner)
    {
        if (cuantas == combinacion.Length)
        {
            //Codigo a poner
        }
        else
        {
            for (int k = menorAPoner; k < n; k++)
            {
                combinacion[cuantas] = k;
                Combinaciones(n, combinacion, cuantas + 1, k + 1);
            }
        }
    }
}


///<summary>Unas variaciones pero no se pueden repetir los elementos en la misma secuencia</summary>
public static class VariationsWORepetition
{
    public static void Generate(int max, int size)
    {
        Generate(new int[size], max, 0, new bool[max]);
    }
    public static void Generate(int[] array, int max, int pos, bool[] visitada)
    {
        if (pos == array.Length)
        {
            //Codigo validador y demas
        }
        else for (int i = 0; i < max; i++)
            {
                if (!visitada[i])
                {
                    array[pos] = i;
                    visitada[i] = true;
                    Generate(array, max, pos + 1, visitada);
                    visitada[i] = false;
                }
            }
    }
}

///<summary>Digamos que tenemos que hacer todas las posibles formas diferentes de armar un array de un tamano n, pero pueden alcanzar un valor maximo de m</summary>
public static class Variations
{
    public static void Generate(int max, int size)
    {
        Generate(new int[size], max, 0);
    }
    public static void Generate(int[] array, int max, int pos)
    {
        if (pos == array.Length)
        {
            //Validador y demas
        }

        for (int i = 0; i < max; i++)
        {
            array[pos] = i;
            Generate(array, max, pos + 1);
        }
    }
}


///<summary>Imaginate unas variaciones pero de tamano no fijo, simplemente tienen que ser menores en tamano que un numero n</summary>
public static class Permutations
{
    public static void Generate(int max, int size)
    {
        for (int i = 1; i < size; i++)
        {
            Variations.Generate(max, i);
        }
    }
}


///<summary>Una implementacion de variaciones sin repeticion</summary>
public class TSP
{
    public static void Viajante(int[] ciudades)
    {
        int x = int.MaxValue;
        Viajante(ciudades, new int[ciudades.Length], 0, new bool[ciudades.Length], ref x);
    }
    public static void Viajante(int[] ciudades, int[] recorrido, int cuantas, bool[] visitada, ref int menorCosto)
    {
        if (cuantas == ciudades.Length)
        {
            int suma = SumarRecorridos(recorrido, recorrido.Length);
            if (suma < menorCosto) menorCosto = suma;
        }
        else
        {
            for (int k = 0; k < ciudades.Length; k++)
            {
                if (!visitada[k])
                {
                    if (SumarRecorridos(recorrido, cuantas) < menorCosto)
                    {
                        visitada[k] = true;
                        recorrido[cuantas] = k;
                        Viajante(ciudades, recorrido, cuantas + 1, visitada, ref menorCosto);
                        visitada[k] = false;
                    }
                }
            }
        }
    }
    public static int SumarRecorridos(int[] recorrudo, int i)
    {
        //Imaginemos que no es asi
        return 5;
    }
}