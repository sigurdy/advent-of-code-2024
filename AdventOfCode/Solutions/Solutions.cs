using System.Diagnostics;

namespace AdventOfCode.Solutions;

public abstract class Solutions
{
    public int DayNumber { get; }
    public string[] InputLines { get; }

    public Solutions(int dayNumber)
    {
        DayNumber = dayNumber;
        
        var filePathInput = $"./Solutions/Day{DayNumber}/input.txt";
        InputLines = (File.Exists(filePathInput)) ? File.ReadAllLines(filePathInput) : throw new InvalidOperationException($"File not found: {filePathInput}");
    }

    public void Run()
    {
        Stopwatch swPart1 = new Stopwatch();
        Stopwatch swPart2 = new Stopwatch();
        
        Console.WriteLine("---");
        swPart1.Start();
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        swPart1.Stop();
        Console.WriteLine($"Time: {swPart1.ElapsedMilliseconds} ms");
        Console.WriteLine("---");

        Console.WriteLine("---");
        swPart2.Start();
        Console.WriteLine($"Part 2: {RunPart2(InputLines)}");
        swPart2.Stop();
        Console.WriteLine($"Time: {swPart2.ElapsedMilliseconds} ms");
        Console.WriteLine("---");
    }
    public abstract long RunPart1(string[] inputLines);
    public abstract long RunPart2(string[] inputLines);
}