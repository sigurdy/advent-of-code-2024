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

    private int CountMasOccurrences(char[,] matrix, char[] word)
    {
        Position position = new Position(0, 0);

        int rows = matrix.GetLength(0) - 1;
        int cols = matrix.GetLength(1) - 1;

        int wordCount = 0;
        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= cols; j++)
            {
                position.UpdatePosition(i, j);

                if (matrix[position.X, position.Y] != 'A') continue;

                wordCount += Helper.CheckForWordDiagonal(matrix, position, word);
            }
        }

        return wordCount;
    }

    private int CountXmasOccurrences(char[,] matrix, char[] word)
    {
        Position position = new Position(0, 0);
        
        int rows = matrix.GetLength(0) - 1;
        int cols = matrix.GetLength(1) - 1;

        int wordCount = 0;
        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= cols; j++)
            {
                position.UpdatePosition(i, j);

                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.N, word);
                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.S, word);
                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.E, word);
                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.W, word);
                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.Ne, word);
                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.Nw, word);
                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.Se, word);
                wordCount += Helper.CheckWordExistsInDirection(matrix, position, Direction.Sw, word);
            }
        }

        return wordCount;
    }

    public override int RunPart1(string[] inputLines)
    {
        int count = CountXmasOccurrences(GenerateMatrix(inputLines), ['X', 'M', 'A', 'S']);
        return count;
    }

    public override int RunPart2(string[] inputLines)
    {
        int count = CountMasOccurrences(GenerateMatrix(inputLines), ['M', 'S']);
        return count;
    }
}
