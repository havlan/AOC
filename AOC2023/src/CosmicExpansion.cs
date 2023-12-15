namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class CosmicExpansion : ISolver
    {
        record Universe {
            public int Size;
            public HashSet<int> EmptyRows;
            public HashSet<int> EmptyCols;
            public List<(int X, int Y)> Galaxies;
            public ulong Expand;

            public Universe()
            {
                Galaxies = new List<(int, int)>();
                EmptyCols = new HashSet<int>();
                EmptyRows = new HashSet<int>();
            }
        };

        Universe FetchInput()
        {
            var universe = new Universe();
            var lines = File.ReadAllLines(this.filename);
            universe.Size = lines.Length;

            // Find the rows & columns we need to 'expand'
            for (int r = 0; r < universe.Size; r++)
            {
                if (!lines[r].ToCharArray().Any(ch => ch != '.'))
                {
                    universe.EmptyRows.Add(r);
                }
            }

            for (int c = 0; c < universe.Size; c++)
            {
                bool empty = true;
                for (int r = 0; r < universe.Size; r++)
                {
                    if (lines[r][c] != '.')
                    {
                        empty = false;
                        break;
                    }
                }
                if (empty)
                {
                    universe.EmptyCols.Add(c);
                }
            }

            for (int r = 0; r < universe.Size; r++)
            {
                for (int c = 0; c < universe.Size; c++)
                {
                    if (lines[r][c] == '#')
                    {
                        universe.Galaxies.Add((r, c));
                    }
                }
            }

            return universe;
        }
        private string filename;
        private Universe universe;

        public CosmicExpansion(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            this.universe = FetchInput();
        }

        Dictionary<((int, int), (int, int)), ulong> CalculateDistancesBetweenGalaxies(Universe galaxy, (int, int) start)
        {
            ulong[,] distances = new ulong[galaxy.Size, galaxy.Size];
            for (int r = 0; r < galaxy.Size; r++)
            {
                for (int c = 0; c < galaxy.Size; c++)
                    distances[r, c] = UInt64.MaxValue;
            }
            distances[start.Item1, start.Item2] = 0;

            var q = new Queue<(int R, int C)>();
            q.Enqueue(start);

            var neighbours = new (int R, int C)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            do
            {
                var curr = q.Dequeue();

                foreach (var n in neighbours)
                {
                    var nr = curr.R + n.R;
                    var nc = curr.C + n.C;

                    // make sure neighbour is in bounds
                    if (nr < 0 || nr >= galaxy.Size || nc < 0 || nc >= galaxy.Size)
                    {
                        continue;
                    }
                    ulong move = galaxy.EmptyRows.Contains(nr) || galaxy.EmptyCols.Contains(nc) ? galaxy.Expand : 1;
                    ulong cost = move + distances[curr.R, curr.C];
                    if (cost < distances[nr, nc])
                    {
                        q.Enqueue((nr, nc));
                        distances[nr, nc] = cost;
                    }
                }
            }
            while (q.Count > 0);

            var allDistances = new Dictionary<((int, int), (int, int)), ulong>();
            foreach (var g in galaxy.Galaxies.Where(g => g != start))
            {
                // add both distances
                allDistances.Add((start, g), distances[g.Item1, g.Item2]);
                allDistances.Add((g, start), distances[g.Item1, g.Item2]);
            }

            return allDistances;
        }

        public void PartOne()
        {
            this.universe.Expand = 2;
            var allDistances = new Dictionary<((int, int), (int, int)), ulong>();

            foreach (var g in universe.Galaxies)
            {
                var distances = CalculateDistancesBetweenGalaxies(this.universe, g);
                foreach (var k in distances.Keys)
                {
                    if (!allDistances.ContainsKey(k))
                    {
                        allDistances.Add(k, distances[k]);
                    }
                }
            }

            ulong total = 0;
            foreach (var d in allDistances.Values)
            {
                total += d;
            }
            Console.WriteLine(total / 2);
        }

        public void PartTwo()
        {
            this.universe.Expand = 1_000_000;
            var allDistances = new Dictionary<((int, int), (int, int)), ulong>();

            foreach (var g in universe.Galaxies)
            {
                var distances = CalculateDistancesBetweenGalaxies(this.universe, g);
                foreach (var k in distances.Keys)
                {
                    if (!allDistances.ContainsKey(k))
                    {
                        allDistances.Add(k, distances[k]);
                    }
                }
            }

            ulong total = 0;
            foreach (var d in allDistances.Values)
            {
                total += d;
            }
            Console.WriteLine(total / 2);
        }
    }
}
