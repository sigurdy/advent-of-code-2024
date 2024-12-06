namespace AdventOfCode.Utils;

public class Map
{
    public char[,] MapMatrix { get; private set; }
    public Position Position { get; private set; }
    public Position BlockPosition { get; set; }
    public HashSet<string> UsedBlocks { get; set; } = new();
    public Map(char[,] map)
    {
        MapMatrix = map;
        Position = GetStartingPosition();
    }

    private Position GetStartingPosition()
    {
        char[] startingSymbols = { '<', '>', '^', 'V' };

        for (int i = 0; i < MapMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < MapMatrix.GetLength(1); j++)
            {
                if (startingSymbols.Contains(MapMatrix[i, j])) return new Position(i, j);
            }
        }
        throw new InvalidOperationException("Unable to find starting position.");
    }

    public void ResetMap(char[,] map)
    {
        MapMatrix = map;
        Position = GetStartingPosition();
        UsedBlocks = new HashSet<string>();
    }


    public void IncrementPosition((int, int) directionVector)
    {
        Position.IncrementPosition(directionVector);
    }

    public char GetSymbolAtPosition(Position position)
    {
        if (IsOutOfBounds(position))
        {
            return '?';
        }
        return MapMatrix[position.X, position.Y];
    }

    public void UpdateMap(char value, Position position)
    {
        var rows = MapMatrix.GetLength(0);
        var cols = MapMatrix.GetLength(1);

        if (IsOutOfBounds(position)) 
            throw new InvalidOperationException($"Can not update position '{position.X}, {position.Y}', out of bound of matrix of size '{rows}, {cols}'");

        MapMatrix[position.X, position.Y] = value;
    }

    private bool IsOutOfBounds(Position position)
    {
        var rows = MapMatrix.GetLength(0) - 1;
        var cols = MapMatrix.GetLength(1) - 1;
        if (position.X > rows || position.X < 0 || position.Y < 0 || position.Y > cols) return true;
        return false;
    }

    public void PrintMap()
    {
        var rows = MapMatrix.GetLength(0);
        var cols = MapMatrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(MapMatrix[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
