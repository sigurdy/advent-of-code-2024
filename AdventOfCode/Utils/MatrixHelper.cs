using AdventOfCode.Solutions.Day4;

namespace AdventOfCode.Utils;

public static class MatrixHelper
{
    public static char[,] GenerateMatrix(string[] inputLines)
    {
        int rows = inputLines.Length;
        int cols = inputLines[0].Length;

        char[,] matrix = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = inputLines[i][j];
            }
        }

        return matrix;
    }

    public static Dictionary<Direction, (int, int)> GetDirections()
    {
        return new()
        {
            { Direction.N, (0, -1) },
            { Direction.Ne, (1, -1) },
            { Direction.E, (1, 0) },
            { Direction.Se, (1, 1) },
            { Direction.S, (0, 1) },
            { Direction.Sw, (-1, 1) },
            { Direction.W, (-1, 0) },
            { Direction.Nw, (-1, -1) },
        };
    }
}
