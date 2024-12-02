namespace AdventOfCode.Solutions;

public abstract class Solutions
{
    public int DayNumber { get; set; }
    public string[] InputLines { get; set; }

    public Solutions(int dayNumber)
    {
        DayNumber = dayNumber;
        
        var filePathInput = $"./Solutions/Day{DayNumber}/input.txt";
        InputLines = (File.Exists(filePathInput)) ? File.ReadAllLines(filePathInput) : throw new InvalidOperationException($"File not found: {filePathInput}");
    }

    public abstract void Run();
    public abstract int RunPart1(string[] inputLines);
    public abstract int RunPart2(string[] inputLines);
}