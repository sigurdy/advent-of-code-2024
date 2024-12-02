using System.Runtime.CompilerServices;

namespace src.Solutions.Day2;

public class Day2 : Solutions
{
    public Day2() : base(2)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
    }

    private bool ChecIfNumberIncreaseCorrect(int number1, int number2)
    {
        int distance = Math.Abs(number1 - number2);

        if (distance > 0 && distance <= 3) return true;
        return false;
    }

    private bool IsIncreasing(int currentNumber, int previousNumber)
    {
        return (currentNumber > previousNumber) ? true : false;
    }

    private bool IsCorrectNumber(int currentNumber, int previousNumber, bool increasing)
    {
        if (increasing != IsIncreasing(currentNumber, previousNumber))
        {
            return false;
        }

        if (!ChecIfNumberIncreaseCorrect(previousNumber, currentNumber))
        {
            return false;
        }
        return true;
    }

    private bool IsSafeLine(string line)
    {
        var numbers = line.Split(' ');
        bool isSafe = true;

        int previousNumber = 0;
        bool isIncreasing = true;

        for (int i = 0; i < numbers.Length; i++)
        {
            var currentNumber = int.Parse(numbers[i].ToString());

            if (i == 0)
            {
                previousNumber = currentNumber;
                continue;
            }
            else if (i == 1)
            {
                isIncreasing = IsIncreasing(currentNumber, previousNumber);
            }

            if (!IsCorrectNumber(currentNumber, previousNumber, isIncreasing))
            {
                isSafe = false;
                break;
            }
            
            previousNumber = currentNumber;
        }
        return isSafe ? true : false;
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
            if (IsSafeLine(line, )) numberOfSafeLevels += 1;
        }

        return numberOfSafeLevels;
    }
}
