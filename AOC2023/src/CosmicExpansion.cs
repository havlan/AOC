namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CosmicExpansion : ISolver
    {
        private string filename;
        private HashSet<int> emptyColumns;
        private int size;
        private HashSet<int> emptyRows;
        private List<(int X, int Y)> galaxies;
        private Dictionary<((int X, int Y), (int X, int Y)), ulong> distance;

        public CosmicExpansion(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            this.size = lines.Length;
            this.emptyRows = new HashSet<int>();

            // empty rows
            for (int r = 0; r < size; r++)
            {
                if (!lines[r].ToCharArray().Any(ch => ch != '.'))
                {
                    this.emptyRows.Add(r);
                }
            }

            // empty columns
            this.emptyColumns = new HashSet<int>();
            for (int c = 0; c < size; c++)
            {
                bool empty = true;
                for (int r = 0; r < size; r++)
                {
                    if (lines[r][c] != '.')
                    {
                        empty = false;
                        break;
                    }
                }
                if (empty)
                {
                    this.emptyColumns.Add(c);
                }
            }

            this.galaxies = new List<(int X, int Y)>();
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (lines[r][c] == '#')
                    {
                        galaxies.Add((c, r));
                    }
                }
            }

            this.distance = new Dictionary<((int X, int Y), (int X, int Y)), ulong>();
        }

        private ulong Shortest1((int X, int Y) start, (int X, int Y) goal, int size)
        {
            var neighbours = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var q = new Queue<(int, int)>();
            q.Enqueue(start);
            var currentDistances = new Dictionary<(int, int), ulong>() { { start, 0 } };
            ulong shortest = ulong.MaxValue;

            do
            {
                var current = q.Dequeue();
                var currentDistance = currentDistances[current];

                foreach (var neighbour in neighbours)
                {
                    var next = (current.Item1 + neighbour.Item1, current.Item2 + neighbour.Item2);
                    if (next.Item1 < 0 || next.Item1 >= size || next.Item2 < 0 || next.Item2 >= size)
                    {
                        continue;
                    }

                    ulong delta = emptyColumns.Contains(next.Item2) || emptyRows.Contains(next.Item1) ? (ulong)2 : 1;
                    var nextCost = currentDistance + delta;

                    if (nextCost > shortest)
                    {
                        continue;
                    }

                    if (next == goal && nextCost < shortest)
                    {
                        currentDistances[goal] = nextCost;
                        shortest = nextCost;
                        continue;
                    }

                    if (!currentDistances.ContainsKey(next))
                    {
                        currentDistances.Add(next, nextCost);
                        q.Enqueue(next);
                    }
                    else if (nextCost < currentDistances[next])
                    {
                        currentDistances[next] = nextCost;
                        q.Enqueue(next);
                    }
                }
            } while (q.Count() > 0);

            return currentDistances[goal];
        }

        public void PartOne()
        {
            var pairnum = 0;
            foreach (var galaxy in galaxies)
            {
                foreach (var otherGalaxy in galaxies)
                {
                    pairnum++;
                    if (galaxy == otherGalaxy)
                    {
                        continue;
                    }

                    if (!distance.ContainsKey((galaxy, otherGalaxy)))
                    {
                        var d = Shortest1(galaxy, otherGalaxy, size);
                        distance.Add((galaxy, otherGalaxy), d);
                        distance.Add((otherGalaxy, galaxy), d);
                    }
                }

                Console.WriteLine($"Done with {galaxy} after {pairnum}/{galaxies.Count() * galaxies.Count()} pairs.");
            }
            ulong total = 0;
            foreach (var d in this.distance.Values)
            {
                total += d;
            }
            Console.WriteLine(total / 2);
        }

        public void PartTwo()
        {
        }
    }
}
