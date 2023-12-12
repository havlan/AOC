using AOC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2023
{
    public class PipeMaze : ISolver
    {
        private string filename;
        Dictionary<string, Pipe> pipeTypes = new Dictionary<string, Pipe>
        {
            ["|"] = new Pipe(0, -1, 0, 1),
            ["-"] = new Pipe(-1, 0, 1, 0),
            ["L"] = new Pipe(0, -1, 1, 0),
            ["J"] = new Pipe(0, -1, -1, 0),
            ["7"] = new Pipe(-1, 0, 0, 1),
            ["F"] = new Pipe(1, 0, 0, 1),
            ["."] = new Pipe(0, 0, 0, 0),
        };
        private string[,] grid;
        private Point start1;

        record Pipe(int AX, int AY, int BX, int BY);

        public PipeMaze(string filename)
        {
            this.filename = filename;
        }
        public void Init()
        {
            var inputs = File.ReadAllLines(this.filename);
            this.grid = new string[inputs.Length, inputs[0].Length];

            this.start1 = new Point();
            for (int y = 0; y < inputs.Length; y++)
            {
                var line = inputs[y].ToCharArray().Select(char.ToString).ToArray();
                for (int x = 0; x < line.Length; x++)
                {
                    grid[y, x] = line[x];
                    if (line[x] == "S")
                    {
                        start1 = new Point(x, y);
                    }
                }
            }
        }

        public void PartOne()
        {
            var start = this.start1;
            var potentials = new List<Point>
            {
                new Point(start.X - 1, start.Y),
                new Point(start.X + 1, start.Y),
                new Point(start.X, start.Y - 1),
                new Point(start.X, start.Y + 1)
            };
        }

        public void PartTwo()
        {
            throw new NotImplementedException();
        }
    }
}
