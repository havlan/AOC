using System;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public class DumboOctopus : ISolver
    {
        private readonly string filename;
        private int[][] data;

        public DumboOctopus(string filename)
        {
            this.filename = filename;
        }

        private int[][] ReadData()
        {
            var allLines = File.ReadAllLines(this.filename);
            return allLines.Select(x => x.Select(c => c - '0').ToArray()).ToArray();
        }

        int Step(int[][] octos)
        {
            var ymax = octos.Length - 1;
            var xmax = octos[0].Length - 1;

            Stack<(int y, int x)> work = new();
            HashSet<(int y, int x)> flashes = new();

            for (int y = 0; y <= ymax; y++)
            {
                for (int x = 0; x <= xmax; x++)
                {
                    IncreaseLevel(y, x);
                }
            }

            while (work.Count > 0)
            {
                var pos = work.Pop();
                for (var y = pos.y - 1; y <= pos.y + 1; y++)
                    for (var x = pos.x - 1; x <= pos.x + 1; x++)
                    {
                        if (x < 0 || x > xmax || y < 0 || y > ymax) continue;
                        if (flashes.Contains((y, x))) continue;

                        IncreaseLevel(y, x);
                    }
            }

            return flashes.Count;

            void IncreaseLevel(int y, int x)
            {
                octos[y][x] = (octos[y][x] + 1) % 10;
                if (octos[y][x] == 0)
                {
                    flashes.Add((y, x));
                    work.Push((y, x));
                }
            }
        }

        public void Init()
        {
            this.data = ReadData();
        }

        public void PartOne()
        {
            var flashesCount = 0;
            var octos = this.data.Select(a => (int[])a.Clone()).ToArray();
            for (var i = 0; i < 100; i++)
            {
                flashesCount += Step(octos);
            }

            Console.WriteLine("{0}", flashesCount);
        }

        public void PartTwo()
        {
            var octos = ReadData();
            var step = 0;
            while (true)
            {
                var numFlashes = Step(octos);
                if (numFlashes == 100)
                {
                    break;
                }
                step++;
            }

            Console.WriteLine("{0}", step + 1);
        }
    }
}