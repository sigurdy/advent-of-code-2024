namespace AdventOfCode.Solutions.Day5;

public class Day5 : Solutions
{
    public Day5() : base(5)
    {
    }

    private Dictionary<int, List<int>> GenerateDictionaryOfRules(string[] arrayOfRules)
    {
        Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
        foreach (var rule in arrayOfRules)
        {
            var number = rule.Split("|");
            int firstNumber = int.Parse(number.First());
            int lastNumber = int.Parse(number.Last());

            var currentRule = lastNumber;

            if (rules.ContainsKey(firstNumber) && rules.TryGetValue(firstNumber, out var values))
            {
                values.Add(currentRule);
            }
            else
            {
                rules.Add(firstNumber, new List<int> { currentRule });
            }
        }

        return rules;
    }

    private int GetMiddleNumberOfCorrectOrders(int[] orderNumbers, Dictionary<int, List<int>> rules)
    {
        Stack<int> remaindingOrderNumbers = new Stack<int>(orderNumbers.Reverse());

        foreach (var key in orderNumbers)
        {
            remaindingOrderNumbers.Pop();
            if (remaindingOrderNumbers.Count == 0) break;

            List<int> allRules = new List<int>();
            bool keyExists = rules.TryGetValue(key, out allRules);

            if (!keyExists)
            {
                return 0;
            }

            foreach (var item in remaindingOrderNumbers)
            {
                if (!allRules.Contains(item))
                {
                    return 0;
                }
            }
        }
        
        return orderNumbers[orderNumbers.Length/2];
    }

    private bool IsCorrectOrder(int[] orderNumbers, Dictionary<int, List<int>> rules)
    {
        Stack<int> remaindingOrderNumbers = new Stack<int>(orderNumbers.Reverse());
        foreach (var key in orderNumbers)
        {
            remaindingOrderNumbers.Pop();
            if (remaindingOrderNumbers.Count == 0) break;

            List<int> allRules = new List<int>();
            bool keyExists = rules.TryGetValue(key, out allRules);

            if (!keyExists)
            {
                return false;
            }

            foreach (var item in remaindingOrderNumbers)
            {
                if (!allRules.Contains(item))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private int[] FixOrder(int[] orderNumbers, Dictionary<int, List<int>> rules)
    {
        int[] newArray = new int[orderNumbers.Length];
        foreach (var key in orderNumbers)
        {
            int[] orderNumbersExceptCurrent = orderNumbers.Where(x => x != key).ToArray();
            bool keyExists = rules.TryGetValue(key, out List<int> allRules);

            if (allRules is null)
            {
                newArray[orderNumbers.Length - 1] = key;
                continue;
            }

            var union = allRules.Intersect(orderNumbersExceptCurrent).ToArray();

            newArray[ orderNumbers.Length - union.Length - 1 ] = key;
        }

        return newArray;
    }

    public override long RunPart1(string[] inputLines)
    {
        var indexEmpty = Array.FindIndex(inputLines, string.IsNullOrEmpty);

        var rules = inputLines.Take(indexEmpty).ToArray();
        var orders = inputLines.Skip(indexEmpty).ToArray();

        var dictRules = GenerateDictionaryOfRules(rules);

        int sum = 0;
        foreach (var order in orders)
        {
            if (string.IsNullOrEmpty(order)) continue;
            int[] orderNumbers = order.Split(",").Select(int.Parse).ToArray();
            sum += GetMiddleNumberOfCorrectOrders(orderNumbers, dictRules);
        }

        return sum;
    }

    public override long RunPart2(string[] inputLines)
    {
        var indexEmpty = Array.FindIndex(inputLines, string.IsNullOrEmpty);

        var rules = inputLines.Take(indexEmpty).ToArray();
        var orders = inputLines.Skip(indexEmpty).ToArray();

        var dictRules = GenerateDictionaryOfRules(rules);

        int count = 0;
        foreach (var order in orders)
        {
            if (string.IsNullOrEmpty(order)) continue;
            int[] orderNumbers = order.Split(",").Select(int.Parse).ToArray();

            if (!IsCorrectOrder(orderNumbers, dictRules))
            {
                var fixedOrder = FixOrder(orderNumbers, dictRules);
                count += fixedOrder[fixedOrder.Length / 2];
            }
        }

        return count;
    }
}
