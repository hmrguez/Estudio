namespace Backtracking;

public class NQueens
{
    public static int UbicaReinas(int n)
    {
        bool[,] tablero = new bool[n, n];
        return UbicaReinas(n, 0, tablero);
    }
    public static int UbicaReinas(int n, int i, bool[,] tablero)
    {
        if (i == n) return 1;
        int total = 0;
        for (int j = 0; j < n; j++)
        {
            tablero[i, j] = true;
            if (EsValido(tablero)) total += UbicaReinas(n, i + 1, tablero);
            tablero[i, j] = false;
        }
        return total;
    }
    public static bool EsValido(bool[,] tablero)
    {
        int[] di = { 1, 1, 1, 0, 0, -1, -1, -1 };
        int[] dj = { 0, 1, -1, 1, -1, 0, 1, -1 };

        for (int i = 0; i < tablero.GetLength(0); i++)
        {
            for (int j = 0; j < tablero.GetLength(1); j++)
            {
                if (!tablero[i, j]) continue;
                for (int d = 0; d < di.Length; d++)
                {
                    for (int k = 1; ; k++)
                    {
                        int ni = i + di[d] * k;
                        int nj = j + dj[d] * k;
                        if (ni < 0 || nj < 0 || ni >= tablero.GetLength(0) || nj >= tablero.GetLength(1)) break;
                        if (tablero[ni, nj]) return false;
                    }
                }
            }
        }
        return true;
    }
}