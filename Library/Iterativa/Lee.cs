namespace Library;

public class Lees
{
    public static int[,] Lee(bool[,] tablero, int filaInicial, int columnaInicial)
    {
        int totalFilas = tablero.GetLength(0);
        int totalColumnas = tablero.GetLength(1);
        int[,] distancias = new int[totalFilas, totalColumnas];
        int[] di = { 1, 1, 1, 0, 0, -1, -1, -1 };
        int[] dj = { 0, 1, -1, 1, -1, 0, 1, -1 };
        bool huboCambio;

        do
        {
            huboCambio = false;
            for (int f = 0; f < totalFilas; f++)
            {
                for (int c = 0; c < totalColumnas; c++)
                {
                    if (distancias[f, c] == 0) continue;
                    if (!tablero[f, c]) continue;
                    for (int d = 0; d < di.Length; d++)
                    {
                        int vf = f + di[d];
                        int vc = c + dj[d];
                        if (PosicionValida(vf, vc, tablero) && tablero[vf, vc] && distancias[vf, vc] == 0)
                        {
                            distancias[vf, vc] = distancias[f, c] + 1;
                            huboCambio = true;
                        }
                    }
                }
            }
        } while (huboCambio);

        return distancias;
    }
    public static bool PosicionValida(int i, int j, bool[,] tablero)
    {
        return i > 0 && j > 0 && i < tablero.GetLength(0) && j < tablero.GetLength(1);
    }
}