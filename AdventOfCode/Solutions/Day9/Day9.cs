namespace AdventOfCode.Solutions.Day9;

public class Day9 : Solutions
{
    public Day9() : base(9)
    {
    }

    private int[] FragmentData(List<int[]> batches)
    {
        int[] output = [];
        foreach (var batch in batches)
        {
            int[] files = new int[batch[1]];
            for (var j = 0; j < batch[1]; j++)
            {
                files[j]= batch[0];
            }
            output = [.. output, .. files];

            int[] empty = new int[batch[2]];
            for (var j = 0; j < batch[2]; j++)
            {
                empty[j] = -1;
            }
            output = [.. output, .. empty];
        }

        return output;
    }

    private void PrintArray(int[] array)
    {
        foreach (var item in array)
        {
            if (item == -1)
            {
                Console.Write(".");
            }
            else 
            {
                Console.Write(item.ToString());
            }
        }
        Console.WriteLine();
    }

    private int[] Defragmentation(int[] array)
    {
        int[] output = (int[]) array.Clone();

        int indexPointer = 0;
        int numberPointer = array.Length - 1;

        while(numberPointer > indexPointer)
        {
            // Swich indexes
            if (output[indexPointer] == -1 && output[numberPointer] != -1)
            {
                int leftNumber = output[indexPointer];
                int rightNumber = output[numberPointer];

                output[indexPointer] = rightNumber;
                output[numberPointer] = leftNumber;
                
                indexPointer++;
                numberPointer--;

                continue;
            }

            if (output[numberPointer] == -1)
            {
                numberPointer--;
            }
            if (output[indexPointer] != -1)
            {
                indexPointer++;
            }
        }

        return output;
    }

    private List<int[]> GenerateBatches(string input)
    {
        List<int[]> batches = new List<int[]>();

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
                batches.Add(batch);
                batch = new int[3];

                id++;
                continue;
            }

            batch[1] = number;

            // If last number add to queue
            if (i == input.Length - 1)
            {
                batch[0] = id;
                batches.Add(batch);
            }
        }

        return batches;
    }

    private long CalculateChecksum(int[] data)
    {
        long sum = 0;
        for (int i = 0; i < data.Length; i++) 
        {
            long id = i;
            long currentNumber = data[i];
            if (currentNumber == -1)
            {
                break;
            }
            long localSum = id * currentNumber;
            sum += localSum;
        }
        return sum;
    }

    public override long RunPart1(string[] inputLines)
    {
        string input = inputLines[0];
        List<int[]> bactches = GenerateBatches(input);
        int[] fragmentedData = FragmentData(bactches);
        int[] defragmentedData = Defragmentation(fragmentedData);
        //PrintArray(fragmentedData);
        //PrintArray(defragmentedData);

        var checksum = CalculateChecksum(defragmentedData);
        return checksum;
    }

    public override long RunPart2(string[] inputLines)
    {
        return 0;
    }
}