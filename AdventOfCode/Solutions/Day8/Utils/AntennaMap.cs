namespace AdventOfCode.Solutions.Day8.Utils;

public class AntennaMap
{
    public char[,] MapMatrix { get; }
    public HashSet<(int, int)> AntinodePositions { get; set; } = new();
    public Dictionary<char, List<(int, int)>> AntennaPosition { get; } = new();

    public AntennaMap(string[] inputLines)
    {
        MapMatrix = InitializeMatrix(inputLines);
    }

    private char[,] InitializeMatrix(string[] inputLines)
    {
        int rows = inputLines.Length;
        int cols = inputLines[0].Length;

        char[,] matrix = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                char currentSymbol = inputLines[i][j];
                matrix[i, j] = currentSymbol;
                if (currentSymbol == '.') continue;
                
                if (AntennaPosition.ContainsKey(currentSymbol))
                {
                    AntennaPosition[currentSymbol].Add( (i,j) );
                }
                else
                {
                    AntennaPosition.Add(currentSymbol, new List<(int, int)> { (i, j) });
                }
            }
        }

        return matrix;
    }

    public bool UpdateAntinode((int x, int y) position)
    {
        int rows = MapMatrix.GetLength(0) - 1;
        int cols = MapMatrix.GetLength(1) - 1;

        if (position.x > rows || position.x < 0 || position.y > cols || position.y < 0) return false;
        
        AntinodePositions.Add(position);
        MapMatrix[position.x, position.y] =
            (MapMatrix[position.x, position.y] == '.') ? '#' : MapMatrix[position.x, position.y];
        return true;
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