namespace AOC2021.src._2022
{
    using AOC;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public class HillClimbingAlgorithm : ISolver
    {
        private string filename;
        private Map map;
        private string[] lines;

        public HillClimbingAlgorithm(string filename)
        {
            this.filename = filename;
            lines = File.ReadAllLines(this.filename);
            map = Map.Parse(lines);
        }

        public void Init()
        {
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", FindPathLength(map, map.Start, map.Goal));
        }

        private int? FindPathLength(Map map, Point start, Point goal)
        {
            var depth = new Dictionary<Point, int>() { [start] = 0 };
            var queue = new Queue<Point>(depth.Keys);
            while (queue.Count > 0)
            {
                var pt = queue.Dequeue();
                if (pt == goal)
                {
                    break;
                }

                var d = depth[pt];
                var adjacent = map
                    .GetValidMoves(pt)
                    .Where(x => !depth.ContainsKey(x));

                foreach (var item in adjacent)
                {
                    depth[item] = d + 1;
                    queue.Enqueue(item);
                }
            }

            return depth.TryGetValue(goal, out var result)
              ? result
              : default(int?);
        }

        public void PartTwo()
        {
            var result = map.Heights
                 .Where(x => x.Value == 1) // all a's
                 .Select(x => FindPathLength(map, x.Key, map.Goal)) // test from all a's
                 .Where(p => p.HasValue) // filter out wrong results
                 .Min(); // shortest path

            Console.WriteLine("{0}", result);
        }

        record struct Point(int X, int Y)
        {
            public IEnumerable<Point> GetAdjacentPoints()
            {
                yield return new(X - 1, Y);
                yield return new(X, Y - 1);
                yield return new(X + 1, Y);
                yield return new(X, Y + 1);
            }
        }

        record Map(ImmutableDictionary<Point, int> Heights, Point Start, Point Goal)
        {
            public IEnumerable<Point> GetValidMoves(Point location)
            {
                if (!Heights.TryGetValue(location, out var height))
                    return Enumerable.Empty<Point>();

                var max = height + 1;
                return location.GetAdjacentPoints()
                    .Where(pt => Heights.TryGetValue(pt, out var height) && height <= max);
            }
            public static Map Parse(string[] input)
            {
                Point start = default,
                      goal = default;
                var result = ImmutableDictionary.CreateBuilder<Point, int>();

                for (var row = 0; row < input.Length; ++row)
                {
                    var line = input[row];
                    for (var col = 0; col < line.Length; ++col)
                    {
                        var pos = new Point(col, row);
                        var ch = line[col];
                        if (ch == 'S')
                        {
                            (result[pos], start) = (1, pos);
                        }
                        else if (ch == 'E')
                        {
                            (result[pos], goal) = (26, pos);
                        }
                        else
                        {
                            result[pos] = ch - 'a' + 1;
                        }
                    }
                }

                return new(result.ToImmutable(), start, goal);
            }
        }
    }
}
