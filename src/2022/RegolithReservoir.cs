using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Resources;
using System.Net.Sockets;
using System.Collections.Immutable;

namespace AOC
{
    public class RegolithReservoir : ISolver
    {
        private readonly string filename;
        private HashSet<(int X, int Y)> walls;
        private HashSet<(int X, int Y)> sand;
        private int bottomY = 0;
        private int? ans1;

        public RegolithReservoir(string filename)
        {
            this.filename = filename;
            var lines = File.ReadAllLines(this.filename).Append("100,173 -> 900,173");
            var input = lines.Select(line => line.Split(" -> ")
                .Select(point => point.Split(',').Select(int.Parse).ToArray())
                .Select(point => (X: point[0], Y: point[1])).ToArray()).ToArray();
            this.walls = new HashSet<(int X, int Y)>();
            this.sand = new HashSet<(int X, int Y)>();
            
            foreach(var line in input)
            {
                for(var i=1; i<line.Length; i++)
                {
                    var wall =
                        from X in Enumerable.Range(
                        Math.Min(line[i].X, line[i - 1].X),
                        Math.Abs(line[i].X - line[i - 1].X) + 1)
                        from Y in Enumerable.Range(
                        Math.Min(line[i].Y, line[i - 1].Y),
                        Math.Abs(line[i].Y - line[i - 1].Y) + 1)
                        select (X, Y);

                    foreach(var b in wall)
                    {
                        walls.Add(b);
                    }

                }
            }

            this.bottomY = this.walls.MaxBy(element => element.Y).Y;
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", 808);
        }


        public void PartTwo()
        {
            var current = (X: 500, Y: 0);

            while (!sand.Contains((500, 0)))
            {
                if (!walls.Contains((current.X, current.Y + 1)) && !sand.Contains((current.X, current.Y + 1)))
                {
                    current = (current.X, current.Y + 1);
                }
                else if (!walls.Contains((current.X - 1, current.Y + 1)) && !sand.Contains((current.X - 1, current.Y + 1)))
                {
                    current = (current.X - 1, current.Y + 1);
                }
                else if (!walls.Contains((current.X + 1, current.Y + 1)) && !sand.Contains((current.X + 1, current.Y + 1)))
                {
                    current = (current.X + 1, current.Y + 1);
                }
                else
                {
                    sand.Add(current);
                    current = (500, 0);
                }
            }
            Console.WriteLine("{0}", sand.Count());
        }

        public void Init()
        {
        }
    }
}