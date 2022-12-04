namespace AOC2022_Day3;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

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
            .Select(x => x[..(x.Length / 2)].Intersect(x[(x.Length / 2)..]).FirstOrDefault())
            .Select(x => (x > 'a' ? x - 'a' : x - 'A' + 26) + 1)
            .Sum();

        return result;
    }
}
