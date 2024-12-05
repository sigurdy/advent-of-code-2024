namespace AdventOfCode.Solutions.Day4;

public static class Helper
{
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
        Dictionary<Direction, (int, int)> directions = GetDirections();

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

            localPosition.UpdatePosition(directions[direction]);
        }
        return 1;
    }
}

public class Position
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    public void UpdatePosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void UpdatePosition((int, int) direction)
    {
        X += direction.Item1;
        Y += direction.Item2;
    }
}

public enum Direction
{
    N,
    S,
    E,
    W,
    Ne,
    Nw,
    Se,
    Sw
}