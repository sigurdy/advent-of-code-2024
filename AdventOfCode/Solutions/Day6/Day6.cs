using AdventOfCode.Solutions.Day6.Utils;

namespace AdventOfCode.Solutions.Day6;

public class Day6 : Solutions
{
    public Day6() : base(6)
    {
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

    private bool NavigateMap(Map map)
    {
        Dictionary<Direction, (int, int)> directions = MatrixHelper.GetDirections();

        while (true)
        {
            // Current Position Settings
            Position currentPosition = map.CarrotPosition;
            char currentCarrotSymbol = map.GetSymbolAtPosition(currentPosition);
            Direction directionOfCarrot = GetDirectionOfCarrot(currentCarrotSymbol);
            
            // It is loop if the current block has been hit at the same direction
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
                map.UpdateMapValueAtPosition(GetCarrotSymbolOfDirection(newDirection), currentPosition);
                continue;
            }

            // Check if Out of Bounds
            if (nextPositionSymbol == '?') return false;

            map.UpdateMapValueAtPosition('X', currentPosition);

            map.IncrementPosition(directions[directionOfCarrot]);
            map.UpdateMapValueAtPosition(currentCarrotSymbol, currentPosition);
        }
    }

    public override long RunPart1(string[] inputLines)
    {
        var mapMatrix = MatrixHelper.GenerateMatrix(inputLines);
        Map map = new Map(mapMatrix);

        bool isLoop = NavigateMap(map);
        if (isLoop) throw new InvalidOperationException("The map is loop");
        
        var numberOfX = CountNumberOfX(map);
        return numberOfX;
    }

    public override long RunPart2(string[] inputLines)
    {
        int numberOfLoops = 0;
        char[,] inputMatrix = MatrixHelper.GenerateMatrix(inputLines);
        
        // Find initial Path
        Map initialMap = new Map((char[,])inputMatrix.Clone()); 
        NavigateMap(initialMap);
        
        // Create copy to not edit the inputMatrix
        Map map = new Map((char[,])inputMatrix.Clone());
        for (int i = 0; i < map.MapMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < map.MapMatrix.GetLength(1); j++)
            {
                Position currentPosition = new Position(i, j);
                var currentPositionSymbol = map.GetSymbolAtPosition(currentPosition);
                
                // Skip if index is not in the path of the carrot
                char charAtCurrentPosition = initialMap.GetSymbolAtPosition(currentPosition);
                if (charAtCurrentPosition != 'X') continue;
                
                // Do not need to place block on start position or already blocked placement
                if (currentPositionSymbol == '#' || map.CarrotSymbols.Contains(currentPositionSymbol)) continue;
                
                map.UpdateMapValueAtPosition('O', currentPosition);

                bool isLoop = NavigateMap(map);
                map.ResetMap((char[,])inputMatrix.Clone());
                if (isLoop) numberOfLoops++;
            }
        }
        return numberOfLoops;
    }
}
