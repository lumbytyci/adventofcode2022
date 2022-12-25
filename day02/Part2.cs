using System.Security.Cryptography.X509Certificates;

namespace AOC2022_Day2;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"A Y
B X
C Z";

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
        return input.Split(Environment.NewLine)
            .Select(x =>
                (Outcome: x[2] == 'X' ? 0 : x[2] == 'Y' ? 3 : 6, Game: x.Replace("X", "A").Replace("Y", "B").Replace("Z", "C")))
            .Select(x => x.Outcome + GetShapeRoundScore(x.Outcome, x.Game))
            .Sum();
    }

    public static int GetShapeRoundScore(int roundScore, string game)
    {
        if (roundScore == 6 && game[0] == 'A') return 2;
        if (roundScore == 6 && game[0] == 'B') return 3;
        if (roundScore == 6 && game[0] == 'C') return 1;
        if (roundScore == 3 && game[0] == 'A') return 1;
        if (roundScore == 3 && game[0] == 'B') return 2;
        if (roundScore == 3 && game[0] == 'C') return 3;
        if (roundScore == 0 && game[0] == 'A') return 3;
        if (roundScore == 0 && game[0] == 'B') return 1;
        if (roundScore == 0 && game[0] == 'C') return 2;

        return 0;
    }
}
