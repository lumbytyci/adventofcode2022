namespace AOC2022_Day7;

public static class Part2
{
    public static void Main(string[] args)
    {
        var input = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

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

    public record Node
    {
        public Node Parent { get; set; }

        public string Name { get; init; }

        public bool IsDirectory { get; init; }

        public ulong Size { get; set; }

        public List<Node> Children { get; init; } = new();

        public override string ToString()
        {
            var path = Name;
            var current = Parent;
            while (current != null)
            {
                path = current.Name + (current.Parent != null ? "/" : string.Empty) + path;
                current = current.Parent;
            }

            return path;
        }
    }

    public static ulong RunPart(string input)
    {
        var root = new Node
        {
            Name = "/",
            IsDirectory = true
        };

        var current = root;

        var lines = input.Split(Environment.NewLine);
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (lines[i].StartsWith("$ ls") && current.Children.Count == 0)
            {
                i++;
                while (i < lines.Length && !lines[i].StartsWith("$"))
                {
                    var newNode = ParseListing(lines[i++]);
                    newNode.Parent = current;
                    current.Children.Add(newNode);
                }
                i--;
            }
            else if (lines[i].StartsWith("$ cd"))
            {
                var newDir = lines[i].Split(" ")[2];
                if (newDir == "..")
                {
                    current = current.Parent;
                }
                else
                {
                    current = current.Children.Where(x => x.Name == newDir).First();
                }
            }
        }

        // DFS
        CalculateDirectorySize(root);

        // BFS
        var queue = new Queue<Node>();
        queue.Enqueue(root);

        const ulong TotalDiskSize = 70_000_000UL;
        const ulong MinimumFreeSpaceSize = 30_000_000UL;
        var currentFreeSpaceSize = TotalDiskSize - root.Size;

        var candidates = new SortedSet<ulong>();
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (currentFreeSpaceSize + node.Size >= MinimumFreeSpaceSize) candidates.Add(node.Size);

            foreach (var child in node.Children.Where(x => x.IsDirectory))
            {
                queue.Enqueue(child);
            }
        }

        return candidates.Min();
    }

    private static Node ParseListing(string line)
    {
        var listing = line.Split(" ");

        var isDirectory = listing[0].StartsWith("dir");
        return new Node
        {
            Name = listing[1],
            Size = !isDirectory ? ulong.Parse(listing[0]) : 0,
            IsDirectory = isDirectory
        };
    }

    private static ulong CalculateDirectorySize(Node node)
    {
        if (!node.IsDirectory) return node.Size;

        var totalSize = 0UL;
        foreach (var child in node.Children)
        {
            totalSize += CalculateDirectorySize(child);
        }

        node.Size = totalSize;

        return totalSize;
    }
}
