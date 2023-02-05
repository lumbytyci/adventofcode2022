namespace AOC2022_Day12;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

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
        var lines = input.Split(Environment.NewLine);
        var grid = new Dictionary<(int x, int y), char>();
        (int x, int y) start = (0, 0), end = (0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                grid[(j, i)] = lines[i][j];
                if (lines[i][j] == 'S')
                {
                    start = (j, i);
                    grid[(j, i)] = 'a';
                }

                if (lines[i][j] == 'E')
                {
                    end = (j, i);
                    grid[(j, i)] = 'z';
                }
            }
        }

        var visited = new HashSet<(int x, int y)>();
        var queue = new Queue<(int cost, (int x, int y) coords)>();
        queue.Enqueue((0, start));

        while (queue.Any())
        {
            var (cost, coords) = queue.Dequeue();
            if (coords.x == end.x && coords.y == end.y) return cost;
            if (visited.Contains(coords)) continue;
            visited.Add(coords);

            foreach (var next in Around(coords))
            {
                if (!grid.ContainsKey(next)) continue;

                if (grid[next] - grid[coords] <= 1)
                {
                    queue.Enqueue((cost + 1, next));
                }
            }
        }

        return default;
    }

    public static IEnumerable<(int x, int y)> Around((int x, int y) coords)
    {
        yield return (coords.x - 1, coords.y);
        yield return (coords.x, coords.y - 1);
        yield return (coords.x + 1, coords.y);
        yield return (coords.x, coords.y + 1);
    }
}
