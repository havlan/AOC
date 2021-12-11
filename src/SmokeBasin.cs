using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public class SmokeBasin : ISolver
    {
        private readonly string filename;
        private int[,] data;

        public SmokeBasin(string filename)
        {
            this.filename = filename;
            this.data = ReadData();
        }

        private int[,] ReadData()
        {
            var lines = File.ReadAllLines(this.filename);
            var result = new int[lines.Length, lines[0].Length];
            for (var i = 0; i < lines.Length; i++)
            {
                for (var k = 0; k < lines[i].Length; k++)
                {
                    result[i, k] = int.Parse(lines[i][k].ToString());
                }
            }

            return result;
        }

        public void PartOne()
        {
            var riskPoints = new List<int>();
            for (int col = 0; col < data.GetLength(0); col++)
            {
                for (int row = 0; row < data.GetLength(1); row++)
                {
                    if (IsMinOfAdjacent(row, col, data))
                    {
                        riskPoints.Add(data[row, col]);
                    }
                }
            }

            Console.WriteLine("{0}", riskPoints.Select(s => s + 1).Sum());
        }

        public bool IsMinOfAdjacent(int i, int j, int[,] data)
        {
            // left, right, up, down
            var current = data[i, j];

            // left
            if (j - 1 >= 0 && current >= data[i, j - 1])
            {
                return false;
            }

            // right
            if (j + 1 < data.GetLength(0) && current >= data[i, j + 1])
            {
                return false;
            }

            // up
            if (i - 1 >= 0 && current >= data[i - 1, j])
            {
                return false;
            }

            //down
            if (i + 1 < data.GetLength(1) && current >= data[i + 1, j])
            {
                return false;
            }

            return true;
        }

        private static int GetValue(int[,] lines, int x, int y)
        {
            if (y < 0 || y >= lines.GetLength(1) || x < 0 || x >= lines.GetLength(0))
            {
                return 9;
            }

            return lines[x, y];
        }

        public void PartTwo()
        {
            List<int> basins = new List<int>();

            for (int col = 0; col < data.GetLength(0); col++)
            {
                for (int row = 0; row < data.GetLength(1); row++)
                {
                    if (IsMinOfAdjacent(col, row, data))
                    {
                        var visited = new HashSet<(int, int)> { (col, row) };
                        FindBasin(data, col, row, visited);
                        basins.Add(visited.Count);
                    }
                }
            }

            Console.WriteLine("{0}", basins.OrderByDescending(s => s).Take(3).Aggregate((t, n) => t * n));
        }

        private static void FindBasin(int[,] lines, int x, int y, HashSet<(int, int)> visited)
        {
            int value = GetValue(lines, x, y);

            int b1 = GetValue(lines, x, y - 1);
            int b2 = GetValue(lines, x, y + 1);
            int b3 = GetValue(lines, x - 1, y);
            int b4 = GetValue(lines, x + 1, y);

            if (b1 < 9 && b1 > value && !visited.Contains((x, y - 1)))
            {
                visited.Add((x, y - 1));

                FindBasin(lines, x, y - 1, visited);
            }

            if (b2 < 9 && b2 > value && !visited.Contains((x, y + 1)))
            {
                visited.Add((x, y + 1));

                FindBasin(lines, x, y + 1, visited);
            }

            if (b3 < 9 && b3 > value && !visited.Contains((x - 1, y)))
            {
                visited.Add((x - 1, y));

                FindBasin(lines, x - 1, y, visited);
            }

            if (b4 < 9 && b4 > value && !visited.Contains((x + 1, y)))
            {
                visited.Add((x + 1, y));

                FindBasin(lines, x + 1, y, visited);
            }
        }
    }
}
