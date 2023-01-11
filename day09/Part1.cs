namespace AOC2022_Day9;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

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

    public record Position(int x, int y)
    {
        public Position Move(string direction, int units)
        {
            return direction switch
            {
                "U" => new Position(x, y + units),
                "R" => new Position(x + units, y),
                "D" => new Position(x, y - units),
                "L" => new Position(x - units, y),
                _ => throw new ArgumentException("Invalid direction.")
            };
        }

        public bool Touches(Position point)
        {
            return Math.Abs(point.x - x) < 2 && Math.Abs(point.y - y) < 2;
        }
    }

    public static int RunPart(string input)
    {
        var movements = input.Split(Environment.NewLine)
            .Select(x => x.Split(" "))
            .Select(x => (Direction: x[0], Units: int.Parse(x[1])));

        var path = new HashSet<Position>();
        var head = new Position(0, 0);
        var tail = new Position(0, 0);
        path.Add(tail);

        foreach (var movement in movements)
        {
            for (int d = 1; d <= movement.Units; d++)
            {
                var newHead = head.Move(movement.Direction, 1);
                if (!newHead.Touches(tail))
                {
                    tail = new Position(head.x, head.y);
                    path.Add(tail);
                }
                head = newHead;
            }
        }

        return path.Count();
    }
}
