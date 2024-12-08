using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Day4;

public class Day4 : Solutions
{
    public Day4() : base(4)
    {
    }

    public static int CheckForWordDiagonal(char[,] matrix, Position position, char[] word)
    {
        int masCount = 0;
        try
        {
            // Acending Diagonal
            char[] swne = [matrix[position.X + 1, position.Y - 1], matrix[position.X - 1, position.Y + 1]];
            if (word.All(x => swne.Contains(x)))
            {
                masCount++;
            }

            // Decending Diagonal
            char[] nwse = [matrix[position.X - 1, position.Y - 1], matrix[position.X + 1, position.Y + 1]];
            if (word.All(x => nwse.Contains(x)))
            {
                masCount++;
            }
        }
        catch (IndexOutOfRangeException)
        {
            return 0;
        }
        if (masCount == 2) return 1;

        return 0;
    }

    public static int CheckWordExistsInDirection(char[,] matrix, Position position, Direction direction, char[] word)
    {
        Dictionary<Direction, (int, int)> directions = MatrixHelper.GetDirections();

        Position localPosition = new Position(position.X, position.Y);
        for (int i = 0; i < word.Length; i++)
        {
            try
            {
                if (matrix[localPosition.X, localPosition.Y] != word[i]) return 0;
            }
            catch (IndexOutOfRangeException)
            {
                return 0;
            }

            localPosition.IncrementPosition(directions[direction]);
        }
        return 1;
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

                wordCount += CheckForWordDiagonal(matrix, position, word);
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

                wordCount += CheckWordExistsInDirection(matrix, position, Direction.N, word);
                wordCount += CheckWordExistsInDirection(matrix, position, Direction.S, word);
                wordCount += CheckWordExistsInDirection(matrix, position, Direction.E, word);
                wordCount += CheckWordExistsInDirection(matrix, position, Direction.W, word);
                wordCount += CheckWordExistsInDirection(matrix, position, Direction.Ne, word);
                wordCount += CheckWordExistsInDirection(matrix, position, Direction.Nw, word);
                wordCount += CheckWordExistsInDirection(matrix, position, Direction.Se, word);
                wordCount += CheckWordExistsInDirection(matrix, position, Direction.Sw, word);
            }
        }

        return wordCount;
    }

    public override long RunPart1(string[] inputLines)
    {
        int count = CountXmasOccurrences(MatrixHelper.GenerateMatrix(inputLines), ['X', 'M', 'A', 'S']);
        return count;
    }

    public override long RunPart2(string[] inputLines)
    {
        int count = CountMasOccurrences(MatrixHelper.GenerateMatrix(inputLines), ['M', 'S']);
        return count;
    }
}
