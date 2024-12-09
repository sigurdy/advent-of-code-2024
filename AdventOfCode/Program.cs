using AdventOfCode.Solutions;
using AdventOfCode.Solutions.Day1;
using AdventOfCode.Solutions.Day2;
using AdventOfCode.Solutions.Day3;
using AdventOfCode.Solutions.Day4;
using AdventOfCode.Solutions.Day5;
using AdventOfCode.Solutions.Day6;
using AdventOfCode.Solutions.Day7;
using AdventOfCode.Solutions.Day8;
using AdventOfCode.Solutions.Day9;

while (true)
{
    // Get all the solutions
    Solutions[] solutions = { new Day1(), new Day2(), new Day3(), new Day4(), new Day5(), new Day6(), new Day7(), new Day8(), new Day9() };
    var possibleDays = solutions.Select(x => x.DayNumber).ToArray();
    
    // Get input from user
    Console.WriteLine($"Enter the day to execute, possible input '{string.Join(", ", possibleDays)}':");
    var userInput = Console.ReadLine();
    int.TryParse(userInput, out int day);
    if (day == 0 || !possibleDays.Contains(day))
    {
        Console.WriteLine($"Please enter a valid day. Day '{day}' is not valid.");
        continue;
    }
    
    // Get Class to Run 
    var dayToRun = solutions.First(x => x.DayNumber == day);
    
    // Execute chosen day
    Console.WriteLine($"----Running day: '{day}' ----");
    Console.WriteLine("Input");
    dayToRun.Run();
    Console.WriteLine();
    Console.WriteLine("----");
}