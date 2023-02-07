namespace AOC2022_Day13;
using System.Text.Json.Nodes;

public static class Part1
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
        var chunks = input.Split($"{Environment.NewLine}{Environment.NewLine}");

        var count = 0;
        var index = 1;
        foreach (var packet in chunks)
        {
            var packetSplit = packet.Split(Environment.NewLine);
            var left = JsonNode.Parse(packetSplit.First());
            var right = JsonNode.Parse(packetSplit.Last());
            count += ComparePackets(left, right) < 0 ? index : 0;
            index++;
        }

        return count;
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

