namespace AdventOfCode.Solutions.Day2;

public class Day2 : Solutions
{
    public Day2() : base(2)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        Console.WriteLine($"Part 2: {RunPart2(InputLines)}");
    }

    private bool IsNumberIncreaseCorrect(int number1, int number2)
    {
        int distance = Math.Abs(number1 - number2);

        if (distance > 0 && distance <= 3) return true;
        return false;
    }

    private int GetNumberOfSafeLine(string[] lines, bool damp = false)
    {
        int numberOfSafeLevels = 0;
        foreach (var line in lines)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToArray();
            bool lineIsSafe = false;
            
            if (damp)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    var dampedArray = numbers.Where( (value, index) => index != i).ToArray();
                    bool currentIsSafe = IsLineSafe(dampedArray);

                    if (currentIsSafe)
                    {
                        lineIsSafe = true;
                        break;
                    }
                }
            }
            else
            {
                lineIsSafe = IsLineSafe(numbers);   
            }

            if (lineIsSafe) numberOfSafeLevels++;
        }

        return numberOfSafeLevels;
    }

    private bool IsLineSafe(int[] numbers, bool damp = false)
    {
        bool isIncreasing = numbers.Last() > numbers.First();

        int previousNumber = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            var currentNumber = numbers[i];
            if (i == 0)
            {
                previousNumber = currentNumber;
                continue;
            }

            if (isIncreasing != (currentNumber > previousNumber))
            {
                return false;
            }

            if (!IsNumberIncreaseCorrect(previousNumber, currentNumber))
            {
                return false;
            }

            previousNumber = currentNumber;
        }

        return true;
    }

    public override int RunPart1(string[] inputLines)
    {
        return GetNumberOfSafeLine(inputLines, damp: false);
    }

    public override int RunPart2(string[] inputLines)
    {
        return GetNumberOfSafeLine(inputLines, damp: true);
    }

}