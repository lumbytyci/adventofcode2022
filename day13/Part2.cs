namespace AOC2022_Day13;

using System.Text.Json.Nodes;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

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
        var dividers = new List<JsonNode> { JsonNode.Parse("[[2]]"), JsonNode.Parse("[[6]]") };
        var packets = input.Split($"{Environment.NewLine}")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => JsonNode.Parse(x))
            .Concat(dividers)
            .ToList();

        packets.Sort(ComparePackets);

        return (packets.IndexOf(dividers[0]) + 1) * (packets.IndexOf(dividers[1]) + 1);
    }

    public static int ComparePackets(JsonNode left, JsonNode right)
    {
        if (left.IsNumber() && right.IsNumber())
        {
            return (int)left - (int)right;
        }

        var newLeft = left as JsonArray ?? new JsonArray { (int)left };
        var newRight = right as JsonArray ?? new JsonArray { (int)right };

        foreach (var pair in Enumerable.Zip(newLeft, newRight))
        {
            var result = ComparePackets(pair.First, pair.Second);

            if (result != 0) return result;
        }

        if (newLeft.Count() != newRight.Count())
        {
            return newLeft.Count() - newRight.Count();
        }

        return 0;
    }
}

public static class MyExtensions
{
    public static bool IsNumber(this object source)
    {
        return source is JsonValue;
    }
}

