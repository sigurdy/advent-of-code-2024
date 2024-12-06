using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Day6;

public class Day6 : Solutions
{
    public Day6() : base(6)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        Console.WriteLine($"Part 2: {RunPart2(InputLines)}");
    }

    private Direction GetDirectionOfCarrot(char carrot)
    {
        switch (carrot)
        {
            case '>':
                return Direction.E;
            case '^':
                return Direction.N;
            case 'V':
                return Direction.S;
            case '<':
                return Direction.W;
            default: throw new InvalidOperationException($"Unknown carrot type of: '{carrot}'");
        }
    }

    private char GetCarrotSymbolOfDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.E:
                return '>';
            case Direction.N:
                return '^';
            case Direction.S:
                return 'V';
            case Direction.W:
                return '<';
            default: throw new InvalidOperationException($"Unknown direction of: '{direction}'");
        }
    }

    private Direction RotateCarrot(Direction direction) 
    { 
        switch (direction)
        {
            case Direction.N:
                return Direction.E;
            case Direction.E:
                return Direction.S;
            case Direction.S:
                return Direction.W;
            case Direction.W:
                return Direction.N;
            default: throw new InvalidOperationException($"Unknown direction '{direction}'");
        }
    }

    private int CountNumberOfX(Map map)
    {
        int count = 0;
        var rows = map.MapMatrix.GetLength(0);
        var cols = map.MapMatrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map.MapMatrix[i, j] == 'X') count++;
            }
        }
        return count;
    }

    private int NavigateMap(Map map)
    {
        Dictionary<Direction, (int, int)> directions = MatrixHelper.GetDirections();
        
        while (true)
        {
            // Current Position Settings
            Position currentPosition = map.Position;
            char currentCarrotSymbol = map.GetSymbolAtPosition(currentPosition);
            Direction directionOfCarrot = GetDirectionOfCarrot(currentCarrotSymbol);

            // Next Position Settings
            Position nextPosition = new Position(currentPosition.X, currentPosition.Y);
            nextPosition.IncrementPosition(directions[directionOfCarrot]);
            char nextPositionSymbol = map.GetSymbolAtPosition(nextPosition);

            // Update Map
            if (nextPositionSymbol == '#')
            {
                Direction newDirection = RotateCarrot(directionOfCarrot);
                map.UpdateMap(GetCarrotSymbolOfDirection(newDirection), currentPosition);
                continue;
            }

            // Check if Complete
            if (nextPositionSymbol == '?')
            {
                break;
            }

            map.UpdateMap('X', currentPosition);

            map.IncrementPosition(directions[directionOfCarrot]);
            map.UpdateMap(currentCarrotSymbol, currentPosition);
        }
        return CountNumberOfX(map) + 1;
    }

    private bool CheckIfLoop(Map map)
    {
        Dictionary<Direction, (int, int)> directions = MatrixHelper.GetDirections();

        while (true)
        {
            // Current Position Settings
            Position currentPosition = map.Position;
            char currentCarrotSymbol = map.GetSymbolAtPosition(currentPosition);
            Direction directionOfCarrot = GetDirectionOfCarrot(currentCarrotSymbol);
            
            // Check if Loop
            if (map.UsedBlocks.Contains($"{currentPosition.X},{currentPosition.Y}{directionOfCarrot}")) return true;

            // Next Position Settings
            Position nextPosition = new Position(currentPosition.X, currentPosition.Y);
            nextPosition.IncrementPosition(directions[directionOfCarrot]);
            char nextPositionSymbol = map.GetSymbolAtPosition(nextPosition);

            // Update Map
            if (nextPositionSymbol == '#' || nextPositionSymbol == 'O')
            {
                map.UsedBlocks.Add($"{currentPosition.X},{currentPosition.Y}{directionOfCarrot}");
                Direction newDirection = RotateCarrot(directionOfCarrot);
                map.UpdateMap(GetCarrotSymbolOfDirection(newDirection), currentPosition);
                continue;
            }

            // Check if Out of Bounds
            if (nextPositionSymbol == '?') return false;

            map.UpdateMap('X', currentPosition);

            map.IncrementPosition(directions[directionOfCarrot]);
            map.UpdateMap(currentCarrotSymbol, currentPosition);
        }
    }

    public override int RunPart1(string[] inputLines)
    {
        var mapMatrix = MatrixHelper.GenerateMatrix(inputLines);
        Map map = new Map(mapMatrix);

        var count = NavigateMap(map);

        return count;
    }

    public override int RunPart2(string[] inputLines)
    {
        var count = 0;
        char[,] inputMatrix = MatrixHelper.GenerateMatrix(inputLines);
        char[] carrotSymbols = { '<', '>', '^', 'V' };

        Map map = new Map(MatrixHelper.GenerateMatrix(inputLines));
        for (int i = 0; i < inputMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < inputMatrix.GetLength(1); j++)
            {
                Position currentPosition = new Position(i, j);
                var currentPositionSymbol = map.GetSymbolAtPosition(currentPosition);

                if (currentPositionSymbol == '#' || carrotSymbols.Contains(currentPositionSymbol)) continue;

                map.BlockPosition = currentPosition;
                map.UpdateMap('O', currentPosition);

                bool isLoop = CheckIfLoop(map);
                map.ResetMap(MatrixHelper.GenerateMatrix(inputLines));
                if (isLoop) count++;
            }
        }

        return count;
    }
}
