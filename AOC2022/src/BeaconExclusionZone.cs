using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using AOC;

namespace AOC_2022
{
    internal record Point(int x, int y) { }
    public class BeaconExclusionZone : ISolver
    {
        private static Regex lineRegEx = new(
            @"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)$",
            RegexOptions.Compiled
        );
        private readonly string filename;
        private Dictionary<Point, Point> sensorsToBeacon;

        private int numBits;

        public BeaconExclusionZone(string filename)
        {
            this.filename = filename;
            this.sensorsToBeacon = new Dictionary<Point, Point>();
            var lines = File.ReadAllLines(filename);

            foreach (var line in lines)
            {
                var match = lineRegEx.Match(line);
                sensorsToBeacon.Add(
                    new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
                    new Point(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)));
            }
        }

        public void PartOne()
        {
            for (var y = 0; y < 4_000_000; y++)
            {
                var ranges = new List<Point>();
                var didFindInLoop = false;
                foreach (var sensorToB in this.sensorsToBeacon)
                {
                    var sensor = sensorToB.Key;
                    var beacon = sensorToB.Value;

                    int dist = getDistance(sensor, beacon);

                    if (isInCorrectRange(sensor, y, dist))
                    {
                        var yDiff = Math.Abs(y - sensor.y);
                        var lowerX = sensor.x - (dist - yDiff) + 1;
                        var upperX = sensor.x + (dist - yDiff) + 1;
                        if (ranges.Any(p => p.x <= lowerX && p.y >= upperX))
                        {
                            // didFindInLoop = true;
                        }

                        ranges.Add(new Point(lowerX, upperX));
                    }
                }

                if (didFindInLoop)
                {
                    continue;
                }

                ranges.Sort((x, y) => x.x.CompareTo(y.x));

                var partTwoX = 0;
                foreach (var point in ranges)
                {
                    if (partTwoX < point.x)
                    {
                        Console.WriteLine("Pt2: {0}", partTwoX * 4_000_000L + y);
                        return;
                    }
                    if (partTwoX < point.y)
                    {
                        partTwoX = point.y;
                    }
                }

                var result = 0;
                if (y == 2_000_000)
                {
                    var currX = ranges.First().x;
                    foreach (var p in ranges)
                    {
                        if (currX >= p.y)
                        {
                            continue;
                        }
                        else if (currX > p.x)
                        {
                            result += p.y - currX;
                        }
                        else
                        {
                            result += (p.y - p.x);
                        }

                        currX = p.y;
                    }

                    Console.WriteLine("{0}", result);
                }
            }

        }


        public void PartTwo()
        {
            Console.WriteLine("{0}", 0);
        }

        public void Init()
        {
        }

        private bool isInCorrectRange(Point sensor, int y, int distance) => sensor.y < y ? sensor.y + distance > y : sensor.y - distance < y;

        private int getDistance(Point sensor, Point beacon) => Math.Abs(beacon.y - sensor.y) + Math.Abs(beacon.x - sensor.x);
    }
}