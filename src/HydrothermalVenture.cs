using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public class HydrothermalVenture : ISolver
    {
        private record struct Point(int X, int Y);

        private readonly string filename;
        private List<(Point start, Point stop)> data;

        public HydrothermalVenture(string filename)
        {
            this.filename = filename;
            this.data = ReadData();
        }

        private List<(Point, Point)> ReadData()
        {
            var data = new List<(Point, Point)>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var startAndEnd = line.Split("->");

                    var startSplit = startAndEnd[0].Split(",");
                    var start = new Point(int.Parse(startSplit[0]), int.Parse(startSplit[1]));

                    var endSplit = startAndEnd[1].Split(",");
                    var end = new Point(int.Parse(endSplit[0]), int.Parse(endSplit[1]));
                    data.Add((start, end));
                }
            }
            return data;
        }

        public void PartOne()
        {
            var pointMap = new Dictionary<Point, int>();
            foreach (var p in this.data)
            {
                if (!IsApplicableLine(p.start, p.stop))
                {
                    continue;
                }

                var dix = Math.Sign(p.stop.X - p.start.X);
                var diy = Math.Sign(p.stop.Y - p.start.Y);

                if (diy == 0)
                {
                    for (var x = p.start.X; x != p.stop.X + dix; x += dix)
                    {
                        var currentPoint = new Point(x, p.stop.Y);
                        if (pointMap.ContainsKey(currentPoint))
                        {
                            pointMap[currentPoint]++;
                        }
                        else
                        {
                            pointMap[currentPoint] = 1;
                        }
                    }
                }

                if (dix == 0)
                {
                    for (var y = p.start.Y; y != p.stop.Y + diy; y += diy)
                    {
                        var currentPoint = new Point(p.stop.X, y);
                        if (pointMap.ContainsKey(currentPoint))
                        {
                            pointMap[currentPoint]++;
                        }
                        else
                        {
                            pointMap[currentPoint] = 1;
                        }
                    }
                }
            }

            Console.WriteLine("{0}", pointMap.Count(s => s.Value >= 2));
        }

        public void PartTwo()
        {
            var pointMap = new Dictionary<Point, int>();
            foreach (var p in this.data)
            {
                var dix = Math.Sign(p.stop.X - p.start.X);
                var diy = Math.Sign(p.stop.Y - p.start.Y);


                var x = p.start.X;
                var y = p.start.Y;

                while (x != p.stop.X + dix || y != p.stop.Y + diy)
                {
                    var currentPoint = new Point(x, y);

                    if (pointMap.ContainsKey(currentPoint))
                    {
                        pointMap[currentPoint]++;
                    }
                    else
                    {
                        pointMap[currentPoint] = 1;
                    }

                    x += dix;
                    y += diy;
                }
            }

            Console.WriteLine("{0}", pointMap.Count(s => s.Value >= 2));
        }


        private bool IsApplicableLine(Point start, Point end)
        {
            return (start.X == end.X) || (start.Y == end.Y);
        }
    }
}