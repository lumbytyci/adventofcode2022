namespace AOC2022_Day9;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

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
    public static Position FollowMove(Position head, Position tail)
    {
        var dx = head.Touches(tail) ? 0 : Math.Sign(head.x - tail.x);
        var dy = head.Touches(tail) ? 0 : Math.Sign(head.y - tail.y);

        return tail with { x = tail.x + dx, y = tail.y + dy };
    }

    public static int RunPart(string input)
    {
        var movements = input.Split(Environment.NewLine)
            .Select(x => x.Split(" "))
            .Select(x => (Direction: x[0], Units: int.Parse(x[1])));


        const int SnakeLength = 10;
        var snake = Enumerable.Repeat(new Position(0, 0), SnakeLength)
            .ToList();

        var path = new HashSet<Position>
        {
            snake[0]
        };

        foreach (var movement in movements)
        {
            for (int d = 1; d <= movement.Units; d++)
            {
                snake[0] = snake[0].Move(movement.Direction, 1);
                for (int i = 1; i < SnakeLength; i++)
                {
                    snake[i] = FollowMove(snake[i - 1], snake[i]);
                }
                path.Add(snake[SnakeLength - 1]);
            }
        }

        return path.Count();
    }
}
