namespace AdventOfCode.Solutions.Day2;

public class Day2 : Solutions
{
    public Day2() : base(2)
    {
    }

    public override void Run()
    {
        //Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        Console.WriteLine($"Part 2: {RunPart2(InputLines)}");
    }

    private bool IsNumberIncreaseCorrect(int number1, int number2)
    {
        int distance = Math.Abs(number1 - number2);

        if (distance > 0 && distance <= 3) return true;
        return false;
    }

    private bool IsNumberIncreasing(int currentNumber, int previousNumber)
    {
        return (currentNumber > previousNumber);
    }

    private bool IsCorrectNumber(int currentNumber, int previousNumber, bool increasing)
    {
        if (increasing != IsNumberIncreasing(currentNumber, previousNumber))
        {
            return false;
        }

        if (!IsNumberIncreaseCorrect(previousNumber, currentNumber))
        {
            return false;
        }
        return true;
    }
    
    // TODO: Create a method to remove each element at a time for a given FAULTY line.

    private bool IsSafeLine(string line, bool damp = false)
    {
        var numbers = line.Split(' ').Select(int.Parse).ToArray();
        bool isIncreasing = IsNumberIncreasing(numbers.Last(), numbers.First());
        
        bool isSafe = false;
        int previousNumber = 0;
        bool usedDamp = false;
        
        for (int i = 0; i < numbers.Length; i++)
        {
            var currentNumber = numbers[i];

            if (i == 0)
            {
                previousNumber = currentNumber;
                continue;
            }

            if (!IsCorrectNumber(currentNumber, previousNumber, isIncreasing))
            {
                if (damp && !usedDamp)
                {
                    if (i == numbers.Length - 1)
                    {
                        isSafe = true;
                        continue;
                    }
                    usedDamp = true;
                    continue;
                }

                isSafe = false;
                break;
            }
            isSafe = true;
            previousNumber = currentNumber;
        }

        if (!isSafe) Console.WriteLine($"Problem: {line}");
        return isSafe;
    }

    public override int RunPart1(string[] inputLines)
    {
        int numberOfSafeLevels = 0;

        foreach (var line in inputLines)
        {
            if (IsSafeLine(line)) numberOfSafeLevels += 1;
        }

        return numberOfSafeLevels;
    }

    public override int RunPart2(string[] inputLines)
    {
        int numberOfSafeLevels = 0;

        foreach (var line in inputLines)
        {
            if (IsSafeLine(line, damp: true)) numberOfSafeLevels += 1;
        }

        return numberOfSafeLevels;
    }
}