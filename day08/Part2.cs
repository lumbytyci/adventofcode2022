namespace AOC2022_Day8;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"30373
25512
65332
33549
35390";

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
        var grid = input.Split(Environment.NewLine)
            .Select(x => x.ToCharArray().Select(x => x - '0').ToArray())
            .ToArray();

        var scenicScores = new SortedSet<int>();
        for (var y = 1; y < grid.Length - 1; y++)
        {
            for (var x = 1; x < grid[0].Length - 1; x++)
            {
                var curr = grid[y][x];
                var dTop = 1;
                var top = grid[y - dTop][x];
                while (y - dTop >= 0)
                {
                    top = grid[y - dTop][x];
                    if (curr <= top) break;
                    dTop++;
                }
                if (curr > top) dTop--;

                var dRight = 1;
                var right = grid[y][x + dRight];
                while (x + dRight < grid[0].Length)
                {
                    right = grid[y][x + dRight];
                    if (curr <= right) break;
                    dRight++;
                }
                if (curr > right) dRight--;

                var dBottom = 1;
                var bottom = grid[y + dBottom][x];
                while (y + dBottom < grid.Length)
                {
                    bottom = grid[y + dBottom][x];
                    if (curr <= bottom) break;
                    dBottom++;
                }
                if (curr > bottom) dBottom--;

                var dLeft = 1;
                var left = grid[y][x - dLeft];
                while (x - dLeft >= 0)
                {
                    left = grid[y][x - dLeft];
                    if (curr <= left) break;
                    dLeft++;
                }
                if (curr > left) dLeft--;

                scenicScores.Add(dTop * dRight * dBottom * dLeft);
            }
        }

        return scenicScores.Max();
    }
}
