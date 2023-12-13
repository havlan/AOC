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
        private string[] inputs;
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
            this.inputs = File.ReadAllLines(this.filename);
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
            (_, var steps) = FindLoop();

            Console.WriteLine(steps);
        }

        public void PartTwo()
        {
            var maze = FetchMaze();
            Fill(maze);
            Console.WriteLine($"P2: {CountInner(maze)}");
        }

        (List<PipePoint>, int) FindLoop()
        {
            int steps = 1;
            var start = this.sequences[start1];
            var a = start.Item1;
            var b = start.Item2;
            PipePoint prevA = start1;
            PipePoint prevB = start1;
            List<PipePoint> loop = new()
            {
                start1, a, b
            };

            do
            {
                var nextA = this.sequences[a].Item1 == prevA ? this.sequences[a].Item2 : this.sequences[a].Item1;
                var nextB = this.sequences[b].Item1 == prevB ? this.sequences[b].Item2 : this.sequences[b].Item1;
                prevA = a;
                prevB = b;
                a = nextA;
                b = nextB;
                loop.Add(a);
                loop.Add(b);
                ++steps;
            } while (a != b);

            return (loop, steps); 
        }

        // Create a scaled out maze
        private char[,] FetchMaze()
        {
            int length = this.inputs.Length;
            int width = this.inputs[0].Length;

            char[,] maze = new char[(length + 1) * 3, (width + 1) * 3];
            for (int r = 0; r < maze.GetLength(0) - 1; r++)
            {
                for (int c = 0; c < maze.GetLength(1) - 1; c++)
                {
                    maze[r, c] = '.';
                }
            }

            for (int r = 0; r < length; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    Scale(r * 3 + 1, c * 3 + 1, this.inputs[r][c], maze);
                }
            }

            return maze;
        }

        private void Fill(char[,] maze)
        {
            var neighbours = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var length = maze.GetLength(0);
            var width = maze.GetLength(1);
            var locs = new Queue<(int, int)>();
            locs.Enqueue((0, 0));
            var visited = new HashSet<(int, int)>();

            do
            {
                var loc = locs.Dequeue();
                if (visited.Contains(loc))
                    continue;
                maze[loc.Item1, loc.Item2] = 'o';
                visited.Add((loc.Item1, loc.Item2));

                foreach (var n in neighbours)
                {
                    var nr = loc.Item1 + n.Item1;
                    var nc = loc.Item2 + n.Item2;
                    if (nr < 0 || nr >= length || nc < 0 || nc >= width || visited.Contains((nr, nc)))
                        continue;
                    if (maze[nr, nc] == '.')
                        locs.Enqueue((nr, nc));
                }
            }
            while (locs.Count > 0);
        }

        private int CountInner(char[,] maze)
        {
            var length = maze.GetLength(0) - 1;
            var width = maze.GetLength(1) - 1;
            int count = 0;
            var pixels = new (int, int)[] { (0, 0), (0, 1), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2) };

            for (int r = 1; r < length; r += 3)
            {
                for (int c = 1; c < width; c += 3)
                {
                    bool isInner = true;
                    foreach (var p in pixels)
                    {
                        if (maze[r + p.Item1, c + p.Item2] == 'o')
                        {
                            isInner = false;
                            break;
                        }
                    }
                    if (isInner) ++count;
                }
            }

            return count;
        }

        /// scale to 3x3
        private void Scale(int row, int col, char ch, char[,] maze)
        {
            (int, int, char)[] pattern = ch switch
            {
                '.' => [(0, 0, '.'),
                    (0, 1, '.'),
                    (0, 2, '.'),
                    (1, 0, '.'),
                    (1, 1, '.'),
                    (1, 2, '.'),
                    (2, 0, '.'),
                    (2, 1, '.'),
                    (2, 2, '.')],
                'S' => [(0, 0, '.'),
                    (0, 1, 'S'),
                    (0, 2, '.'),
                    (1, 0, 'S'),
                    (1, 1, 'S'),
                    (1, 2, 'S'),
                    (2, 0, '.'),
                    (2, 1, 'S'),
                    (2, 2, '.')],
                '|' => [(0, 0, '.'),
                    (0, 1, '|'),
                    (0, 2, '.'),
                    (1, 0, '.'),
                    (1, 1, '|'),
                    (1, 2, '.'),
                    (2, 0, '.'),
                    (2, 1, '|'),
                    (2, 2, '.')],
                '-' => [(0, 0, '.'),
                    (0, 1, '.'),
                    (0, 2, '.'),
                    (1, 0, '-'),
                    (1, 1, '-'),
                    (1, 2, '-'),
                    (2, 0, '.'),
                    (2, 1, '.'),
                    (2, 2, '.')],
                'L' => [(0, 0, '.'),
                    (0, 1, '|'),
                    (0, 2, '.'),
                    (1, 0, '.'),
                    (1, 1, '+'),
                    (1, 2, '-'),
                    (2, 0, '.'),
                    (2, 1, '.'),
                    (2, 2, '.')],
                'J' => [(0, 0, '.'),
                    (0, 1, '|'),
                    (0, 2, '.'),
                    (1, 0, '-'),
                    (1, 1, '+'),
                    (1, 2, '.'),
                    (2, 0, '.'),
                    (2, 1, '.'),
                    (2, 2, '.')],
                '7' => [(0, 0, '.'),
                    (0, 1, '.'),
                    (0, 2, '.'),
                    (1, 0, '-'),
                    (1, 1, '+'),
                    (1, 2, '.'),
                    (2, 0, '.'),
                    (2, 1, '|'),
                    (2, 2, '.')],
                'F' => [(0, 0, '.'),
                    (0, 1, '.'),
                    (0, 2, '.'),
                    (1, 0, '.'),
                    (1, 1, '+'),
                    (1, 2, '-'),
                    (2, 0, '.'),
                    (2, 1, '|'),
                    (2, 2, '.')],
                _ => throw new InvalidOperationException()
            };

            foreach (var p in pattern)
            {
                maze[row + p.Item1, col + p.Item2] = p.Item3;
            }
        }
    }
}
