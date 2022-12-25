namespace AOC2022_Day4;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

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
        var result = input.Split(Environment.NewLine)
            .Select(x => x.Split(","))
            .Select(x => (left: x[0].Split("-"), right: x[1].Split("-")))
            .Select(x => (left: (start: int.Parse(x.left[0]), end: int.Parse(x.left[1])), right: (start: int.Parse(x.right[0]), end: int.Parse(x.right[1]))))
            .Where(x =>
                (x.left.start >= x.right.start && x.left.end <= x.right.end) ||
                (x.left.start <= x.right.start && x.left.end >= x.right.end) ||
                (x.left.start <= x.right.start && x.left.end >= x.right.start) ||
                (x.left.start >= x.right.start && x.left.start <= x.right.end)
            )
            .Count();

        return result;
    }
}
