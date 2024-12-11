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
    
    private void PrintAllBatches(List<Batch> batches)
    {
        foreach (var batch in batches)
        {
            batch.PrintBatch();
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

    private List<int> DefragmentationPart1(List<int> array)
    {
        List<int> output = new List<int>(array);

        int indexPointer = 0;
        int numberPointer = array.Count - 1;

        while (numberPointer > indexPointer)
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

    private List<Batch> DefragmentationPart2(List<Batch> batches)
    {
        List<Batch> newBatches = new List<Batch>(batches);

        Batch sourceBatch = newBatches.Last();
        while (sourceBatch.Id > 0)
        {
            Batch? replacableBatch = newBatches
                .Where(x => x.FreeSpace >= sourceBatch.Files.Length) // the batch has enough free space
                .OrderBy(x => x.Id) // order by id 
                .FirstOrDefault(); // get the smallest id
            if (replacableBatch == null)
            {
                var nextBatch = newBatches.FirstOrDefault(x => x.Id == sourceBatch.Id - 1);
                if (nextBatch == null) break;
                sourceBatch = nextBatch;
                continue;
            }

            replacableBatch.InsertNumbers(sourceBatch.Files);
            newBatches.Remove(sourceBatch);
            sourceBatch = newBatches.Last();
        }

        return newBatches;
    }

    private List<Batch> GenerateBatches(string input)
    {
        List<Batch> batches = new List<Batch>();

        int id = 0;
        for (int i = 0; i < input.Length; i += 2)
        {
            int files = int.Parse(input[i].ToString());
            if (i + 1 >= input.Length)
            {
                int[] lastNumber = [.. Enumerable.Repeat(id, files).ToArray()];
                batches.Add(new Batch(id, lastNumber));
                break;
            }

            int free = int.Parse(input[i + 1].ToString());

            int[] numbers = [.. Enumerable.Repeat(id, files).ToArray(), .. Enumerable.Repeat(-1, free).ToArray()];

            batches.Add(new Batch(id, numbers));
            id++;
        }

        return batches;
    }

    private long CalculateChecksum(List<int> data)
    {
        long sum = 0;
        for (int i = 0; i < data.Count; i++)
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
        List<Batch> batches = GenerateBatches(input);
        List<int> fragmentedData = FragmentData(batches);
        List<int> defragmentedData = DefragmentationPart1(fragmentedData);
        //PrintData(fragmentedData);
        //PrintData(defragmentedData);

        var checksum = CalculateChecksum(defragmentedData);
        return checksum;
    }

    public override long RunPart2(string[] inputLines)
    {
        string input = inputLines[0];
        List<Batch> batches = GenerateBatches(input);
        List<Batch> defragmentedData = DefragmentationPart2(batches);
        PrintAllBatches(defragmentedData);
        
        return 0;
    }
}

public class Batch
{
    public int Id { get; }
    public int FreeSpace { get; private set; }
    public int[] Files { get; private set; }
    public int[] Numbers { get; private set; }

    public Batch(int id, int[] numbers)
    {
        Id = id;
        Numbers = numbers;
        FreeSpace = GetFreeSpace();
        Files = GetFiles();
    }

    public void PrintBatch()
    {
        foreach (var number in Numbers)
        {
            if (number == -1)
            {
                Console.Write(".");
            }
            else
            {
                Console.Write(number);
            }
        }
    }

    public void UpdateBatch(int[] numbers)
    {
        Numbers = numbers;
        FreeSpace = GetFreeSpace();
        Files = GetFiles();
    }

    public void InsertNumbers(int[] numbers)
    {
        if (numbers.Length > FreeSpace) throw new IndexOutOfRangeException("Cannot insert more numbers!");
        int[] newNumbers = (int[])Numbers.Clone();

        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = 0; j < newNumbers.Length; j++)
            {
                if (newNumbers[j] != -1)
                {
                    continue;
                }
                newNumbers[j] = numbers[i];
                break;
            }
        }
        
        Numbers = newNumbers;
        FreeSpace = GetFreeSpace();
        Files = GetFiles();
    }

    private int GetFreeSpace()
    {
        return Numbers.Count(x => x == -1);
    }
    private int[] GetFiles()
    {
        return Numbers.Where(x => x != -1).ToArray();
    }
}