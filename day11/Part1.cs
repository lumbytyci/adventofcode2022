namespace AOC2022_Day11;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

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

    public record Monkey
    {
        public Queue<int> Items { get; init; }

        public Func<int, int> DoOperation { get; init; }

        public Func<int, int> FindTurn { get; init; }
    }

    public static int RunPart(string input)
    {
        var sections = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var monkies = new Dictionary<int, Monkey>();

        var id = 0;
        foreach (var section in sections)
        {
            var lines = section.Split(Environment.NewLine);
            var startingItems = lines[1].Split(':')[1].TrimStart().Split(',').Select(x => int.Parse(x)).ToList();
            var operationSplit = lines[2].Split(' ');
            var num = int.Parse(lines[3].Split(' ').Last());

            // Mother of closures
            var truePass = int.Parse(lines[4].Split(' ').Last());
            var falsePass = int.Parse(lines[5].Split(' ').Last());
            var findTurn = (int worryLevel) =>
            {
                return worryLevel % num == 0 ? truePass : falsePass;
            };

            var leftOperand = operationSplit[5][0] == 'o' ? 0 : int.Parse(operationSplit[5]);
            var rightOperand = operationSplit[7][0] == 'o' ? 0 : int.Parse(operationSplit[7]);
            var op = operationSplit[6];
            var operation = (int old) =>
            {
                var left = leftOperand > 0 ? leftOperand : old;
                var right = rightOperand > 0 ? rightOperand : old;
                return op == "+" ? left + right : left * right;
            };

            monkies[id++] = new Monkey
            {
                Items = new Queue<int>(startingItems),
                DoOperation = operation,
                FindTurn = findTurn
            };
        }

        var monkeyBusiness = Enumerable.Range(0, monkies.Count).ToDictionary(x => x, x => 0);
        const int Rounds = 20;
        for (int i = 0; i < Rounds; i++)
        {
            for (int monkeyId = 0; monkeyId < monkies.Count; monkeyId++)
            {
                var currentMonkey = monkies[monkeyId];
                monkeyBusiness[monkeyId] += currentMonkey.Items.Count;
                while (currentMonkey.Items.Any())
                {
                    var newWorryLevel = (int)Math.Floor(currentMonkey.DoOperation(currentMonkey.Items.Dequeue()) / 3.0D);
                    var receiverMonkeyId = currentMonkey.FindTurn(newWorryLevel);
                    monkies[receiverMonkeyId].Items.Enqueue(newWorryLevel);
                }
            }
        }

        return monkeyBusiness.Values.ToList().OrderByDescending(x => x).Take(2).Aggregate(1, (x, y) => x * y);
    }
}
