using AOC; using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace AOC_2021
{
    public class PassagePathing : ISolver
    {
        private readonly string filename;
        private Dictionary<string, List<string>> graph;

        public PassagePathing(string filename)
        {
            this.filename = filename;
        }

        private Dictionary<string, List<string>> ReadData()
        {
            var lines = File.ReadAllLines(this.filename);

            var connections =
                from line in lines
                let parts = line.Split("-")
                let caveA = parts[0]
                let caveB = parts[1]
                from connection in new[] { (From: caveA, To: caveB), (From: caveB, To: caveA) }
                select connection;

            // grouped by "from":
            return (
                from p in connections
                group p by p.From into g
                select g
            ).ToDictionary(g => g.Key, g => g.Select(connnection => connnection.To).ToList());
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", Explore());
        }

        public void PartTwo()
        {
            Console.WriteLine("{0}", Explore(true));
        }

        int Explore(bool part2 = false)
        {
            var map = this.graph;

            // Recursive approach this time.
            int pathCount(string currentCave, ImmutableHashSet<string> visitedCaves, bool anySmallCaveWasVisitedTwice)
            {

                if (currentCave == "end")
                {
                    return 1;
                }

                var res = 0;
                foreach (var cave in map[currentCave])
                {
                    var isBigCave = cave.ToUpper() == cave;
                    var seen = visitedCaves.Contains(cave);

                    if (!seen || isBigCave)
                    {
                        // we can visit big caves any number of times, small caves only once
                        res += pathCount(cave, visitedCaves.Add(cave), anySmallCaveWasVisitedTwice);
                    }
                    else if (part2 && !isBigCave && cave != "start" && !anySmallCaveWasVisitedTwice)
                    {
                        // part 2 also lets us to visit a single small cave twice (except for start and end)
                        res += pathCount(cave, visitedCaves, true);
                    }
                }
                return res;
            }

            return pathCount("start", ImmutableHashSet.Create<string>("start"), false);
        }

        public void Init()
        {
            this.graph = ReadData();
        }
    }
}