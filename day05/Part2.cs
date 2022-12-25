using System.Text.RegularExpressions;

namespace AOC2022_Day5;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

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

    public static string RunPart(string input)
    {
        var splitInput = input.Split(Environment.NewLine + Environment.NewLine);
        var firstState = splitInput[0].Split(Environment.NewLine);
        var movements = splitInput[1];

        var positionsLine = firstState.Last();
        var positions = positionsLine.Split(" ")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToDictionary(x => int.Parse(x), x => positionsLine.IndexOf(x[0]));

        var stacks = Enumerable.Range(0, positions.Count).Select(x => new Stack<char>()).ToList();

        var firstStateParsed = firstState
            .Reverse()
            .Skip(1)
            .ToList();

        foreach (var row in firstStateParsed)
        {
            for (int i = 1; i <= positions.Count; i++)
            {
                if (row[positions[i]] != ' ')
                {
                    stacks[i - 1].Push(row[positions[i]]);
                }
            }
        }

        foreach (var movement in movements.Split(Environment.NewLine))
        {
            var match = Regex.Match(movement, @"^move (\d+) from (\d+) to (\d+)$");
            var count = int.Parse(match.Groups[1].Value);
            var from = int.Parse(match.Groups[2].Value) - 1;
            var to = int.Parse(match.Groups[3].Value) - 1;

            var moved = new List<char>();
            for (int i = 0; i < count; i++)
            {
                moved.Add(stacks[from].Pop());
            }

            for (int i = count - 1; i >= 0; i--)
            {
                stacks[to].Push(moved[i]);
            }
        }

        return string.Join(string.Empty, stacks.Select(x => x.Peek()));
    }
}
