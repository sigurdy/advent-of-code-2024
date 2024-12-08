using System.Diagnostics;

namespace AdventOfCode.Solutions.Day7;

public class Day7 : Solutions
{
    public Day7() : base(7)
    {
    }

    public override void Run()
    {
        Stopwatch swPart1 = new Stopwatch();
        Stopwatch swPart2 = new Stopwatch();
        swPart1.Start();
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        swPart1.Stop();
        Console.WriteLine($"Part 1 Time: {swPart1.ElapsedMilliseconds}");

        swPart2.Start();
        Console.WriteLine($"Part 2: {RunPart2(InputLines)}");
        swPart2.Stop();
        Console.WriteLine($"Part 2 Time: {swPart2.ElapsedMilliseconds}");
    }

    private double CalculateTwoNumbers(double number1, double number2, char symbol)
    {
        switch (symbol)
        {
            case '*':
                return number1 * number2;
            case '+':
                return number1 + number2;
            case '|':
            {
                string newNumberString = $"{number1}{number2}";
                return double.Parse(newNumberString);
            }
            default:
                throw new InvalidOperationException($"Unknown symbol '{symbol}'");
        }
    }

    private bool CheckIfNumbersCanCalculateToResult(double resultValue, int[] numbers, char[] mathOperators)
    {
        int numberOfOperations = numbers.Length - 1; // This is all the locations the operators can be placed
        int numberOfCombinations =
            (int)Math.Pow(mathOperators.Length, numberOfOperations); // The number of combinations to receive

        // Loop through all combinations
        for (int i = 0; i < numberOfCombinations; i++)
        {
            double currentResult = 0;
            int currentOpPosition = i;

            // Check all Operations
            for (int j = 0; j < numberOfOperations; j++)
            {
                char op = mathOperators[currentOpPosition % mathOperators.Length];

                double currentNumber = (j == 0) ? numbers[j] : currentResult;
                double nextNumber = numbers[j + 1];

                currentResult = CalculateTwoNumbers(currentNumber, nextNumber, op);

                currentOpPosition /= mathOperators.Length;
            }

            if (currentResult == resultValue) return true;
        }

        return false;
    }

    private (double, int[]) GetData(string equation)
    {
        double resultValue = double.Parse(equation.Split(':').First());

        string[] stringNumbers = equation.Split(':').Last().Split(" ").Where(x => x.Trim() != "").ToArray();
        int[] numbers = stringNumbers.Select(x => int.Parse(x.ToString())).ToArray();

        return (resultValue, numbers);
    }

    public override int RunPart1(string[] inputLines)
    {
        char[] mathOperators = { '+', '*' };
        double sum = 0;
        foreach (var equation in inputLines)
        {
            (double resultValue, int[] numbers) = GetData(equation);
            bool isCorrect = CheckIfNumbersCanCalculateToResult(resultValue, numbers, mathOperators);
            if (isCorrect) sum += resultValue;
        }

        Console.WriteLine($"Part 1: {sum}");
        return 0;
    }

    public override int RunPart2(string[] inputLines)
    {
        char[] mathOperators = { '+', '*', '|' };
        double sum = 0;
        foreach (var equation in inputLines)
        {
            (double resultValue, int[] numbers) = GetData(equation);
            bool isCorrect = CheckIfNumbersCanCalculateToResult(resultValue, numbers, mathOperators);
            if (isCorrect) sum += resultValue;
        }

        Console.WriteLine($"Part 2: {sum}");
        return 0;
    }
}