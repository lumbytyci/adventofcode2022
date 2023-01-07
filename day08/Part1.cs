namespace AOC2022_Day8;

public static class Part1
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

        var hiddenCount = 0;
        for (var y = 1; y < grid.Length - 1; y++)
        {
            for (var x = 1; x < grid[0].Length - 1; x++)
            {
                var dTop = 1;
                var dRight = 1;
                var dBottom = 1;
                var dLeft = 1;
                while ((x - dLeft) >= 0 && (x + dRight) < grid[0].Length && (y - dTop) >= 0 && (y + dBottom) < grid.Length)
                {
                    var curr = grid[y][x];
                    var top = grid[y - dTop][x];
                    var right = grid[y][x + dRight];
                    var bottom = grid[y + dBottom][x];
                    var left = grid[y][x - dLeft];

                    dTop = curr <= top ? dTop : dTop + 1;
                    dRight = curr <= right ? dRight : dRight + 1;
                    dBottom = curr <= bottom ? dBottom : dBottom + 1;
                    dLeft = curr <= left ? dLeft : dLeft + 1;

                    if (curr <= top && curr <= right && curr <= bottom && curr <= left)
                    {
                        hiddenCount++;
                        break;
                    }
                }
            }
        }

        return grid.Length * grid[0].Length - hiddenCount;
    }
}
