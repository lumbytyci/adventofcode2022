namespace AOC2022_Day6;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";

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
        var stream = input.AsSpan();
        for (int i = 0; i <= stream.Length - 4; i++)
        {
            var markerSpan = stream.Slice(i, 4);
            if (markerSpan.ToArray().Distinct().Count() == 4) return i + 4;
        }

        return default;
    }
}
