namespace AdventOfCode.Solutions.Day4;

public class Day4 : Solutions
{
    public Day4() : base(4)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        Console.WriteLine($"Part 2: {RunPart2(InputLines)}");
    }

    private char[,] GenerateMatrix(string[] inputLines)
    {
        int rows = inputLines.Length;
        int cols = inputLines[0].Length;

        char[,] matrix = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i,j] = inputLines[i][j];
            }
        }

        return matrix;
    }

    private int CountNumberOfXmas(char[,] matrix)
    {
        int edgeBuffer = "XMAS".Length - 1;
        int rows = matrix.GetLength(0) - 1;
        int cols = matrix.GetLength(1) - 1;

        int xmasCount = 0;
        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= cols; j++)
            {
                if (matrix[i, j] != 'X') continue;
                
                // Check Horizontal
                if (j <= cols - edgeBuffer)
                {
                    if (matrix[i, j] == 'X' && matrix[i, j + 1] == 'M' && matrix[i, j + 2] == 'A' && matrix[i, j + 3] == 'S')
                    {
                        xmasCount ++;
                    }
                }

                // Check Vertical Up
                if (i >= edgeBuffer)
                {
                    if (matrix[i, j] == 'X' && matrix[i - 1, j] == 'M' && matrix[i - 2, j] == 'A' && matrix[i - 3, j] == 'S')
                    {
                        xmasCount ++;
                    }
                }

                // Check Vertical Down
                if (i <= rows - edgeBuffer)
                {
                    if (matrix[i, j] == 'X' && matrix[i + 1, j] == 'M' && matrix[i + 2, j] == 'A' && matrix[i + 3, j] == 'S')
                    {
                        xmasCount++;
                    }
                }

                // Check Backwards
                if (j >= edgeBuffer)
                {
                    if (matrix[i, j] == 'X' && matrix[i, j - 1] == 'M' && matrix[i, j - 2] == 'A' && matrix[i, j - 3] == 'S')
                    {
                        xmasCount++;
                    }
                }

                xmasCount += CountXmasDiagonal(matrix, i, j);
            }
        }

        return xmasCount;
    }

    private int CountXmasDiagonal(char[,] matrix, int rowIndex, int colIndex)
    {
        int xmasDiagonalCount = 0;

        int edgeBuffer = "XMAS".Length - 1;
        int rows = matrix.GetLength(0) - 1;
        int cols = matrix.GetLength(1) - 1;

        // Check Down Right
        if (rowIndex <= rows - edgeBuffer && colIndex <= cols - edgeBuffer )
        {
            if (matrix[rowIndex, colIndex] == 'X' && matrix[rowIndex + 1, colIndex + 1] == 'M' && matrix[rowIndex + 2, colIndex + 2] == 'A' && matrix[rowIndex + 3, colIndex + 3] == 'S')
            {
                xmasDiagonalCount++;
            }
        }

        // Check Down Left
        if (rowIndex <= rows - edgeBuffer && colIndex >= edgeBuffer)
        {
            if (matrix[rowIndex, colIndex] == 'X' && matrix[rowIndex + 1, colIndex - 1] == 'M' && matrix[rowIndex + 2, colIndex - 2] == 'A' && matrix[rowIndex + 3, colIndex - 3] == 'S')
            {
                xmasDiagonalCount++;
            }
        }

        // Check Up Right
        if (rowIndex >= edgeBuffer && colIndex <= cols - edgeBuffer)
        {
            if (matrix[rowIndex, colIndex] == 'X' && matrix[rowIndex - 1, colIndex + 1] == 'M' && matrix[rowIndex - 2, colIndex + 2] == 'A' && matrix[rowIndex - 3, colIndex + 3] == 'S')
            {
                xmasDiagonalCount++;
            }
        }

        // Check Up Left
        if (rowIndex >= edgeBuffer && colIndex >= edgeBuffer)
        {
            if (matrix[rowIndex, colIndex] == 'X' && matrix[rowIndex - 1, colIndex - 1] == 'M' && matrix[rowIndex - 2, colIndex - 2] == 'A' && matrix[rowIndex - 3, colIndex - 3] == 'S')
            {
                xmasDiagonalCount++;
            }
        }

        return xmasDiagonalCount;
    }

    private int CountMas(char[,] matrix)
    {
        int buffer = 1;
        int rows = matrix.GetLength(0) - 1;
        int cols = matrix.GetLength(1) - 1;

        int xmasCount = 0;
        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= cols; j++)
            {
                if (matrix[i, j] != 'A') continue;
                
                if (i < buffer || i > rows - buffer) continue;
                if (j < buffer || j > rows - buffer) continue;
                
                if(CheckMasMatrix(matrix, i, j)) xmasCount++;
            }
        }

        return xmasCount;
    }

    private bool CheckMasMatrix(char[,] matrix, int rowIndex, int colIndex)
    {
        int masCount = 0;
        if (matrix[rowIndex - 1, colIndex - 1] == 'M' && matrix[rowIndex + 1, colIndex + 1] == 'S' || matrix[rowIndex - 1, colIndex - 1] == 'S' && matrix[rowIndex + 1, colIndex + 1] == 'M')
        {
            masCount++;
        }
        
        if (matrix[rowIndex + 1, colIndex - 1] == 'M' && matrix[rowIndex - 1, colIndex + 1] == 'S' || matrix[rowIndex + 1, colIndex - 1] == 'S' && matrix[rowIndex - 1, colIndex + 1] == 'M')
        {
            masCount++;
        }
        
        if (masCount == 2) return true;
        return false;
    }
    

    public override int RunPart1(string[] inputLines)
    {
        int count = CountNumberOfXmas(GenerateMatrix(inputLines));
        return count;
    }

    public override int RunPart2(string[] inputLines)
    {
        int count = CountMas(GenerateMatrix(inputLines));
        return count;
    }
}
