namespace AdventOfCode.Solutions.Day9;

public class Day9 : Solutions
{
    public Day9() : base(9)
    {
    }

    private void PrintData(List<int> array)
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

    private List<int> FragmentData(List<Batch> batches)
    {
        List<int> output = new List<int>();
        foreach (var batch in batches)
        {
            output.AddRange(batch.Numbers);
        }

        return output;
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
    
    private int[] DefragmentationBlock(int[] array, List<int[]> batches)
    {
        int[] output = (int[]) array.Clone();
        List<int[]> newBatches = new List<int[]>(batches);
        
        int indexPointer = batches.First()[0];
        int numberPointer = batches.Last()[0];

        int index = 0;
        while(numberPointer > indexPointer)
        {
            var lastBatch = newBatches[numberPointer];
            var replacableBatch = newBatches
                .Where(x => x[2] >= lastBatch[1]) // the batch has enough free space
                .OrderBy(x => x[0]) // select the IDs
                .FirstOrDefault(); // get the smallest id
            
        }

        return output;
    }

    private List<Batch> GenerateBatches(string input)
    {
        List<Batch> batches = new List<Batch>();

        int id = 0;
        for (int i = 0; i < input.Length; i += 2)
        {
            int files = int.Parse(input[i].ToString());
            if (i+1 >= input.Length)
            {
                int[] lastNumber = [.. Enumerable.Repeat(id, files).ToArray()];
                batches.Add(new Batch(id, lastNumber));
                break;
            }
            int free = int.Parse(input[i+1].ToString());

            int[] numbers = [ .. Enumerable.Repeat(id, files).ToArray(), .. Enumerable.Repeat(-1, free).ToArray() ];

            batches.Add( new Batch(id, numbers) );
            id++;
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
        List<Batch> bactches = GenerateBatches(input);
        List<int> fragmentedData = FragmentData(bactches);
        List<int> defragmentedData = Defragmentation(fragmentedData);
        PrintData(fragmentedData);
        //PrintArray(defragmentedData);

        //var checksum = CalculateChecksum(defragmentedData);
        return 0;
    }

    public override long RunPart2(string[] inputLines)
    {
        //string input = inputLines[0];
        //List<Batch> bactches = GenerateBatches(input);
        //int[] fragmentedData = FragmentData(bactches);
        //int[] defragmentedData = DefragmentationBlock(fragmentedData, bactches);
        return 0;
    }
}

public class Batch
{
    public int Id { get; }
    public int FreeSpace { get; }
    public int[] Numbers { get; }

    public Batch(int id, int[] numbers)
    {
        Id = id;
        Numbers = numbers;
        FreeSpace = numbers.Count(x => x == -1);
    }

    public void UpdateBatch(int[] numbers)
    {

    }
}
