using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day3;

public class Day3 : Solutions
{
    public Day3() : base(3)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        Console.WriteLine($"Part 2: {RunPart2(InputLines)}");
    }

    private List<(int, int)> ExtractValidMults(string input)
    {
        var regExPatternOnlyMults = @"mul\(\d{1,3},\d{1,3}\)";
        var matches = Regex.Matches(input, regExPatternOnlyMults);

        List<(int, int)> items = [];
        foreach (Match match in matches) 
        {
            var value = match.Value;
            var number = Regex.Matches(value, @"\d{1,3}");

            if (number.Count != 2) throw new InvalidOperationException($"Expected only 2 numbers. Found {number.Count}");
            items.Add((int.Parse(number.First().Value), int.Parse(number.Last().Value)));
        }

        if (matches.Count != items.Count) throw new InvalidOperationException("Found matches are not equal");
        Console.WriteLine($"Found '{matches.Count}' matches.");
        return items;
    }

    private List<(int, int)> ExtractValidDosAndDonts(string input)
    {
        var regExPatternOnlyMults = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)";
        var matches = Regex.Matches(input, regExPatternOnlyMults);

        bool enableCalculation = true;
        List<(int, int)> items = [];
        foreach (Match match in matches)
        {
            var value = match.Value;

            if (value == "do()")
            {
                enableCalculation = true;
                continue;
            }
            else if (value == "don't()")
            {
                enableCalculation = false;
                continue;
            }

            if (enableCalculation)
            {
                var number = Regex.Matches(value, @"\d{1,3}");
                if (number.Count != 2) throw new InvalidOperationException($"Expected only 2 numbers. Found {number.Count}");
                items.Add((int.Parse(number.First().Value), int.Parse(number.Last().Value)));
            }
        }

        return items;
    }

    private int CalculateMults(string inputString)
    {
        var validMults = ExtractValidMults(inputString);

        var sum = 0;
        foreach (var mult in validMults)
        {
            sum += mult.Item1 * mult.Item2;
        }

        return sum;
    }

    private int CalculateMultsDoAndDont(string inputString)
    {
        var validMults = ExtractValidDosAndDonts(inputString);

        var sum = 0;
        foreach (var mult in validMults)
        {
            sum += mult.Item1 * mult.Item2;
        }

        return sum;
    }

    public override int RunPart1(string[] inputLines)
    {
        string combinedString = string.Empty;
        foreach (var line in inputLines)
        {
            combinedString += line;
        }
        int result = CalculateMults(combinedString);
        return result;
    }

    public override int RunPart2(string[] inputLines)
    {
        string combinedString = string.Empty;
        foreach (var line in inputLines)
        {
            combinedString += line;
        }
        int result = CalculateMultsDoAndDont(combinedString);
        return result;
    }
}
