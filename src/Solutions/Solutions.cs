namespace src.Solutions;

public abstract class Solutions
{
    public int DayNumber { get; set; }
    public string[] InputLines { get; set; }
    
    public string[]? Part1ExampleLines { get; set; }
    public string[]? Part2ExampleLines { get; set; }

    public Solutions(int dayNumber)
    {
        DayNumber = dayNumber;
        
        var filePath = $"./Solutions/Day{DayNumber}/input.txt";
        InputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException($"File not found: {filePath}");
        
        var filePathPart1 = $"./Solutions/Day{DayNumber}/Example/part1.txt";
        Part1ExampleLines = (File.Exists(filePathPart1)) ? File.ReadAllLines(filePathPart1) : null;
        
        var filePathPart2 = $"./Solutions/Day{DayNumber}/Example/part2.txt";
        Part2ExampleLines = (File.Exists(filePathPart2)) ? File.ReadAllLines(filePathPart2) : null;
    }

    public abstract void Run();
    
    public abstract int RunPart1(string[] inputLines);
    public abstract int RunPart2(string[] inputLines);
    
    public void RunExample()
    {
        var resultPart1Example = 0;
        var resultPart2Example = 0;
        if (Part1ExampleLines != null)
        {
            resultPart1Example = RunPart1(Part1ExampleLines);
            Console.WriteLine($"Part 1: {resultPart1Example}");
        }

        if (Part2ExampleLines != null)
        {
            resultPart2Example = RunPart2(Part2ExampleLines);
            Console.WriteLine($"Part 2: {resultPart2Example}");
        }
    }
}