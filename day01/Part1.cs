namespace AOC2022_Day1;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

        /* Run with puzzle input if any argument is supplied */
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
        return input.Split(Environment.NewLine + Environment.NewLine)
            .Select(x => x.Split(Environment.NewLine).Select(x => int.Parse(x)).Sum())
            .Max();
    }
}
