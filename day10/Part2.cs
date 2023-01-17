using System.Text;

namespace AOC2022_Day10;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";

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

    public static string RunPart(string input)
    {
        var clock = 0;
        var spritePos = 1;
        const int ScreenWidthPx = 40;
        const int ScreenHeightPx = 6;
        var sprite = new HashSet<int>();

        foreach (var line in input.Split(Environment.NewLine))
        {
            var dSpritePos = 0;
            var cycleIncr = 1;
            if (line.Length > 4)
            {
                dSpritePos = int.Parse(line[line.LastIndexOf(" ")..]);
                cycleIncr = 2;
            }

            for (int i = 0; i < cycleIncr; i++)
            {
                if (spritePos - 1 <= (clock % ScreenWidthPx) && (clock % ScreenWidthPx) <= spritePos + 1)
                {
                    sprite.Add(clock);
                }
                clock++;
            }

            spritePos += dSpritePos;
        }

        var output = new StringBuilder(Environment.NewLine);
        for (int i = 0; i < ScreenWidthPx * ScreenHeightPx; i++)
        {
            output.Append(sprite.Contains(i) ? "#" : ".");
            if ((i + 1) % ScreenWidthPx == 0) output.Append(Environment.NewLine);
        }

        return output.ToString();
    }
}
