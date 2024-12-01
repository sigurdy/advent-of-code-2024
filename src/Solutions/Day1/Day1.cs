namespace src.Solutions.Day1;

public class Day1 : Solutions
{
    public Day1() : base(1)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Part 1: {RunPart1(InputLines)}");
        Console.WriteLine($"Part 1: {RunPart2(InputLines)}");
    }

    private int DistanceBetweenNumbers(int number1, int number2)
    {
        return Math.Abs(number1 - number2);
    }

    public override int RunPart1(string[] inputLines)
    {
        int[] firstColumn = inputLines.Select(x => int.Parse(x.Split(" ").First())).ToArray();
        int[] lastColumn = inputLines.Select(x => int.Parse(x.Split(" ").Last())).ToArray();
        
        Array.Sort(firstColumn);
        Array.Sort(lastColumn);
        
        int sumOfDistances = 0;

        for (int i = 0; i < firstColumn.Length; i++)
        {
            sumOfDistances += DistanceBetweenNumbers(firstColumn[i], lastColumn[i]);
        }
        
        return sumOfDistances;
    }

    public override int RunPart2(string[] inputLines)
    {
        int[] firstColumn = inputLines.Select(x => int.Parse(x.Split(" ").First())).ToArray();
        int[] lastColumn = inputLines.Select(x => int.Parse(x.Split(" ").Last())).ToArray();

        int similarityScore = 0;
        foreach (var number in firstColumn)
        {
            var occurences = lastColumn.Count(x => x == number);
            similarityScore += number * occurences;
        }

        return similarityScore;
    }
}