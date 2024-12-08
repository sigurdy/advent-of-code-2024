namespace AdventOfCode.Solutions.Day7;

public class Day7 : Solutions
{
    public Day7() : base(7)
    {
    }

    private long CalculateTwoNumbers(long number1, long number2, char symbol)
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
                return long.Parse(newNumberString);
            }
            default:
                throw new InvalidOperationException($"Unknown symbol '{symbol}'");
        }
    }

    private bool CheckIfNumbersCanCalculateToResult(long resultValue, int[] numbers, char[] mathOperators)
    {
        int numberOfOperations = numbers.Length - 1; // This is all the locations the operators can be placed
        int numberOfCombinations =
            (int)Math.Pow(mathOperators.Length, numberOfOperations); // The number of combinations to receive

        // Loop through all combinations
        for (int i = 0; i < numberOfCombinations; i++)
        {
            long currentResult = 0;
            int currentOpPosition = i;

            // Check all Operations
            for (int j = 0; j < numberOfOperations; j++)
            {
                char op = mathOperators[currentOpPosition % mathOperators.Length];

                long currentNumber = (j == 0) ? numbers[j] : currentResult;
                long nextNumber = numbers[j + 1];

                currentResult = CalculateTwoNumbers(currentNumber, nextNumber, op);

                currentOpPosition /= mathOperators.Length;
            }

            if (currentResult == resultValue) return true;
        }

        return false;
    }

    private (long, int[]) GetData(string equation)
    {
        long resultValue = long.Parse(equation.Split(':').First());

        string[] stringNumbers = equation.Split(':').Last().Split(" ").Where(x => x.Trim() != "").ToArray();
        int[] numbers = stringNumbers.Select(x => int.Parse(x.ToString())).ToArray();

        return (resultValue, numbers);
    }

    public override long RunPart1(string[] inputLines)
    {
        char[] mathOperators = { '+', '*' };
        long sum = 0;
        foreach (var equation in inputLines)
        {
            (long resultValue, int[] numbers) = GetData(equation);
            bool isCorrect = CheckIfNumbersCanCalculateToResult(resultValue, numbers, mathOperators);
            if (isCorrect) sum += resultValue;
        }
        return sum;
    }

    public override long RunPart2(string[] inputLines)
    {
        char[] mathOperators = { '+', '*', '|' };
        long sum = 0;
        foreach (var equation in inputLines)
        {
            (long resultValue, int[] numbers) = GetData(equation);
            bool isCorrect = CheckIfNumbersCanCalculateToResult(resultValue, numbers, mathOperators);
            if (isCorrect) sum += resultValue;
        }
        return sum;
    }
}