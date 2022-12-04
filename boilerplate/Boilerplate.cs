namespace AOC2022_Day1;

public static class Boilerplate 
{
    public static void Main(string[] args)
    {
        var input = @"";

        /* Run with sample input if any argument is supplied */
        if (args.Length == 1)
        {
            input = File.ReadAllText("./input.txt");
        }

        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();
        var result = RunPart(input);
        Console.WriteLine($"Result: {result}. Execution time: {watch.ElapsedMilliseconds} ms");

        Console.ReadKey();
    }

    public static int RunPart(string input)
    {
        /* Implement solution */
        return 0;
    }
}