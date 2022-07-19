namespace Backtracking;
public class Laberynth
{
    public static bool HaySalida(bool[,] laberinto)
    {
        return HaySalida(laberinto,0,0,laberinto.GetLength(0)-1,laberinto.GetLength(1)-1);
    }
    public static bool HaySalida(bool[,] laberinto, int orig_i, int orig_j, int salida_i, int salida_j)
    {
        if (orig_i == salida_i && orig_j == salida_j) return true;
        int[] di = { 1, -1, 0, 0 };
        int[] dj = { 0, 0, -1, 1 };
        for (int d = 0; d < di.Length; d++)
        {
            int i = orig_i + di[d];
            int j = orig_j + dj[d];

            if (i < 0 || j < 0 || i >= laberinto.GetLength(0) || j >= laberinto.GetLength(1)) continue;

            if (!laberinto[i, j])
            {
                laberinto[i, j] = true;
                if (HaySalida(laberinto, i, j, salida_i, salida_j)) return true;
                laberinto[i, j] = false;
            }
        }
        return false;
    }
}