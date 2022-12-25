namespace AOC2022_Day6;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";

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
        const int markerLength = 14;
        var stream = input.AsSpan();
        for (int i = 0; i <= stream.Length - markerLength; i++)
        {
            var markerSpan = stream.Slice(i, markerLength);
            if (markerSpan.ToArray().Distinct().Count() == markerLength) return i + markerLength;
        }

        return default;
    }
}
