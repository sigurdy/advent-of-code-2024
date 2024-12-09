namespace AdventOfCode.Solutions.Day9;

public class Day9 : Solutions
{
    public Day9() : base(9)
    {
    }

    private string GenerateData(Queue<int[]> queue)
    {
        var output = string.Empty;
        foreach (var batch in queue)
        {
            for (var j = 0; j < batch[1]; j++)
            {
                output += batch[0];
            }

            for (var j = 0; j < batch[2]; j++)
            {
                output += ".";
            }
        }

        return output;
    }

    private string RemoveEmpty(string input)
    {
        string output = string.Empty;

        int indexPointer = -1;
        int numberPointer = input.Length - 1;
        for (var i = 0; i < input.Length; i++)
        {
            indexPointer++;

            var num1 = input[indexPointer];
            var num2 = input[numberPointer];
            if (indexPointer >= numberPointer - 1) break;

            if (input[indexPointer] != '.')
            {
                output += input[indexPointer];
                continue;
            }

            if (input[numberPointer] != '.')
            {
                output += input[numberPointer];
                numberPointer--;
            }
        }

        return output;
    }

    private Queue<int[]> GenerateQueue(string input)
    {
        Queue<int[]> queue = new Queue<int[]>();

        int id = 0;
        int[] batch = new int[3];

        for (int i = 0; i < input.Length; i++)
        {
            int number = int.Parse(input[i].ToString());

            // Even number, i.e., the free space
            if (i % 2 != 0)
            {
                batch[0] = id;
                batch[2] = number;
                queue.Enqueue(batch);
                batch = new int[3];

                id++;
                continue;
            }

            batch[1] = number;

            // If last number add to queue
            if (i == input.Length - 1)
            {
                batch[0] = id;
                queue.Enqueue(batch);
            }
        }

        return queue;
    }

    public override long RunPart1(string[] inputLines)
    {
        var input = inputLines[0];
        var queue = GenerateQueue(input);
        var rawData = GenerateData(queue);
        Console.WriteLine(rawData);
        var orderedData = RemoveEmpty(rawData);
        Console.WriteLine(orderedData);
        return 0;
    }

    public override long RunPart2(string[] inputLines)
    {
        return 0;
    }
}