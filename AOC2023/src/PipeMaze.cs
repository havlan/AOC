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
        Dictionary<char, Pipe> pipeTypes = new Dictionary<char, Pipe>
        {
            ['|'] = new Pipe(0, -1, 0, 1),
            ['-'] = new Pipe(-1, 0, 1, 0),
            ['L'] = new Pipe(0, -1, 1, 0),
            ['J'] = new Pipe(0, -1, -1, 0),
            ['7'] = new Pipe(-1, 0, 0, 1),
            ['F'] = new Pipe(1, 0, 0, 1),
            ['.'] = new Pipe(0, 0, 0, 0),
        };
        private string[,] grid;
        private PipePoint start1;
        private Dictionary<PipePoint, (PipePoint, PipePoint)> sequences;

        record Pipe(int AX, int AY, int BX, int BY);
        record PipePoint(int X, int Y);

        public PipeMaze(string filename)
        {
            this.filename = filename;
        }
        public void Init()
        {
            var inputs = File.ReadAllLines(this.filename);
            this.grid = new string[inputs.Length, inputs[0].Length];

            this.start1 = new PipePoint(-1, -1);
            this.sequences = new Dictionary<PipePoint, (PipePoint, PipePoint)>();
            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    if (inputs[y][x] == '.')
                    {
                        continue;
                    }

                    if (inputs[y][x] == 'S')
                    {
                        start1 = new PipePoint(x, y);
                    }
                    else
                    {
                        var dir = pipeTypes[inputs[y][x]];
                        this.sequences.Add(new PipePoint(x, y), (new PipePoint(x + dir.AX, y + dir.AY), new PipePoint(x + dir.BX, y + dir.BY)));
                    }
                }
            }

            var startAdj = this.sequences.Keys.Where(k => this.sequences[k].Item1 == start1 || this.sequences[k].Item2 == start1).ToList();
            this.sequences.Add(start1, (startAdj[0], startAdj[1]));
        }

        public void PartOne()
        {
            int steps = 1;
            var start = this.sequences[start1];
            var a = start.Item1;
            var b = start.Item2;
            PipePoint prevA = start1;
            PipePoint prevB = start1;

            do
            {
                var nextA = this.sequences[a].Item1 == prevA ? this.sequences[a].Item2 : this.sequences[a].Item1;
                var nextB = this.sequences[b].Item1 == prevB ? this.sequences[b].Item2 : this.sequences[b].Item1;
                prevA = a;
                prevB = b;
                a = nextA;
                b = nextB;
                ++steps;
            } while (a != b);

            Console.WriteLine(steps);
        }

        public void PartTwo()
        {
        }
    }
}
