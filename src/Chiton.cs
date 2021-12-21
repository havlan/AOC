using System;
using System.Text;
using System.Collections.Generic;

namespace AOC
{
    public class Chiton : ISolver
    {
        private string filename;

        private int[,] smallMaze;

        private int[,] largeMaze;

        public Chiton(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            smallMaze = new int[lines.Length, lines[0].Length];
            int y = 0, x = 0;
            foreach (var line in lines)
            {
                foreach (var c in line)
                {
                    smallMaze[y, x] = c - '0';
                    x++;
                }
                x = 0;
                y++;
            }

            largeMaze = new int[smallMaze.GetLength(0) * 5, smallMaze.GetLength(1) * 5];
            for (y = 0; y < smallMaze.GetLength(0); y++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (x = 0; x < smallMaze.GetLength(1); x++)
                        {
                            int val = smallMaze[y, x];
                            largeMaze[x + (j * smallMaze.GetLength(0)), y + (i * smallMaze.GetLength(0))] = val + i + j - ((val + i + j - 1) / 9 * 9);
                        }
                    }
                }
            }
        }

        public void PartOne()
        {
            RunDijkstra(smallMaze);
        }

        public void PartTwo()
        {
            RunDijkstra(largeMaze);
        }

        private bool IsInMaze(int x, int y, int[,] maze)
        {
            if (y < 0 || y >= maze.GetLength(1) || x < 0 || x >= maze.GetLength(0))
            {
                return false;
            }

            return true;
        }

        private IEnumerable<(int x, int y)> Neighbors((int x, int y) currentPoint, int[,] maze)
        {
            foreach (var dir in Directions)
            {
                var point = (currentPoint.x + dir.x, currentPoint.y + dir.y);
                if (IsInMaze(point.Item1, point.Item2, maze))
                {
                    yield return point;
                }
            }
        }

        private static (int x, int y)[] Directions = new (int, int)[] { (-1, 0), (0, -1), (1, 0), (0, 1) };


        void RunDijkstra(int[,] maze)
        {
            var pq = new PriorityQueue<(int x, int y), int>();
            pq.Enqueue((0, 0), maze[0, 0]);

            var costMap = new Dictionary<(int x, int y), int>()
            {
                {(0, 0), maze[0, 0]}
            };
            var pathMap = new Dictionary<(int x, int y), (int x, int y)>()
            {
                {(0, 0), (0, 0)}
            };

            (int x, int y) goal = (maze.GetLength(0) - 1, maze.GetLength(1) - 1);

            while (pq.Count > 0)
            {
                var currNode = pq.Dequeue();

                if (currNode == goal)
                {
                    break;
                }

                foreach (var neighbor in Neighbors(currNode, maze))
                {
                    var cost = costMap[currNode] + maze[neighbor.y, neighbor.x];

                    if ((costMap.TryGetValue(neighbor, out var currentNeighborCost) && cost < currentNeighborCost) || !costMap.ContainsKey(neighbor))
                    {
                        pathMap[neighbor] = currNode;
                        costMap[neighbor] = cost;
                        pq.Enqueue(neighbor, cost);
                    }
                }
            }

            var sum = 0;
            var current = goal;
            while (current != (0, 0))
            {
                sum += maze[current.y, current.x];
                current = pathMap[current];
            }

            Console.WriteLine("{0}", sum);
        }
    }
}