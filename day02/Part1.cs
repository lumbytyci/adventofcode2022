namespace AOC2022_Day2;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"A Y
B X
C Z";

        /* Run with sample input if any argument is supplied */
        //if (args.Length == 1)
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
        var lines = input.Replace("X", "A")
            .Replace("Y", "B")
            .Replace("Z", "C")
            .Split(Environment.NewLine);

        var rules = new Dictionary<string, int>
        {
            ["A A"] = 3 + 1,
            ["B B"] = 3 + 2,
            ["C C"] = 3 + 3,
            ["C A"] = 6 + 1,
            ["A B"] = 6 + 2,
            ["B C"] = 6 + 3,
        };

        return lines.Select(x => rules.TryGetValue(x, out var score) ? score : x[2] - ('A' - 1))
            .Sum();
    }
}
