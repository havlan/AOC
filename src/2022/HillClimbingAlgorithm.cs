namespace AOC2021.src._2022
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HillClimbingAlgorithm : ISolver
    {
        private string filename;
        private int[,] map;
        private string[] lines;

        public HillClimbingAlgorithm(string filename)
        {
            this.filename = filename;
            lines = File.ReadLines(this.filename).ToArray();
            map = new int[lines.Length, lines.First().Length];
            for (var i=0;i<lines.Length;i++)
            {
                for(var k = 0; k < lines[i].Length; k++)
                {
                    map[i, k] = lines[i][k] switch
                    {
                        'S' => (int)'a' - 97,
                        'E' => (int)'z' - 97,
                        _ => (int)lines[i][k] - 97,
                    };
                }
            }
        }

        private int GetScore(char c) => 
            c switch
            {
                'S' => (int)'a' - 97,
                'E' => (int)'z' - 97,
                _ => (int)c - 97,
            };

        public void Init()
        {
        }

        public void PartOne()
        {
            (int, int) error = (-1, -1);
            var startState = (0, 0);
            var targetState = (-1, -1);
            var previous = new Dictionary<(int x, int y), (int x, int y)>();
            var trail = new Queue<(int x, int y)>();
            trail.Enqueue(startState);
            while(trail.Count > 0)
            {
                var currentPosition = trail.Dequeue();

                if (lines[currentPosition.x][currentPosition.y] == 'E')
                {
                    Console.WriteLine("Found target at {0},{1}", currentPosition.x, currentPosition.y);
                    targetState = currentPosition;
                    break;
                }

                var neighbors = getNeighbors(currentPosition);
                foreach((int x, int y) neighbor in neighbors)
                {
                    Console.WriteLine("{0}-{1}", neighbor.x, neighbor.y);
                    if (previous.ContainsKey(neighbor))
                    {
                        continue;
                    }

                    previous[neighbor] = currentPosition;
                    trail.Enqueue(neighbor);
                }

            }

            List<(int, int)> path = new List<(int, int)>();
            if (previous.ContainsKey(targetState) || targetState == startState)
            {
                while (targetState != error)
                {
                    path.Add(targetState);
                    targetState = previous.ContainsKey(targetState) ? previous[targetState] : error;
                }
            }

            Console.WriteLine("Path {0}", path.Count);
        }

        private ImmutableArray<(int, int)> getNeighbors ((int row, int column) current)
        {
            var error = (-1, -1);
            var left = current.column > 0 ? (current.row, current.column - 1) : error;
            var right = current.column < lines[0].Length ? (current.row, current.column + 1) : error;
            var up = current.row > 0 ? (current.row - 1, current.column) : error;
            var down = current.row < lines.Length ? (current.row + 1, current.column) : error;

            var neighbors = new[] { left, right, up, down };

            return neighbors.Where(s => s != error).ToImmutableArray();
        }

        public void PartTwo()
        {
        }
    }
}
