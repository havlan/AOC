using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public class Dive : ISolver
    {
        private readonly string filename;
        private List<(string direction, int step)> data;

        public Dive(string filename)
        {
            this.filename = filename;
        }

        private List<(string, int)> ReadData()
        {
            var data = new List<(string, int)>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var dataSplit = line.Split(" ");
                    data.Add((dataSplit[0], int.Parse(dataSplit[1])));
                }
            }
            return data;
        }

        public void PartOne()
        {
            int xPos = 0;
            int depth = 0;
            foreach (var dir in data)
            {
                if (dir.direction == "forward")
                {
                    xPos += dir.step;
                }
                else if (dir.direction == "down")
                {
                    depth += dir.step;
                }
                else if (dir.direction == "up")
                {
                    depth -= dir.step;
                }
            }

            Console.WriteLine("{0}", xPos * depth);
        }

        public void PartTwo()
        {
            int aim = 0;
            int xPos = 0;
            int depth = 0;
            foreach (var dir in data)
            {
                if (dir.direction == "forward")
                {
                    xPos += dir.step;
                    depth += aim * dir.step;
                }
                else if (dir.direction == "down")
                {
                    aim += dir.step;
                }
                else if (dir.direction == "up")
                {
                    aim -= dir.step;
                }
            }

            Console.WriteLine("{0}", xPos * depth);
        }

        public void Init()
        {
            this.data = ReadData();
        }
    }
}