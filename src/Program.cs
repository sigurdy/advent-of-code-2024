using System.Reflection;
using src.Solutions;

while (true)
{
    // Get all the solutions
    var solutions = Assembly.GetExecutingAssembly().DefinedTypes
        .Where(x => x.IsAssignableTo(typeof(Solutions)) && x.Name != "Solutions")
        .Select(x => (Solutions)x.GetConstructor(new Type[0]).Invoke(new object[0]));
    
    // Get input from user
    Console.WriteLine("Enter the day to execute:");
    var userInput = Console.ReadLine();
    int.TryParse(userInput, out int day);
    if (day == 0 || solutions.Any(x => x.DayNumber != day))
    {
        Console.WriteLine($"Please enter a valid day. Day '{day}' is not valid.");
        continue;
    }
    
    // Get Class to Run 
    var dayToRun = solutions.First(x => x.DayNumber == day);
    
    // Execute chosen day
    Console.WriteLine($"----Running day: '{day}' ----");
    Console.WriteLine("Example");
    dayToRun.RunExample();
    Console.WriteLine();
    Console.WriteLine("Input");
    dayToRun.Run();
    Console.WriteLine();
    Console.WriteLine("----");
}