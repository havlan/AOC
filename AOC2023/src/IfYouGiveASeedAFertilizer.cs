namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Formats.Tar;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class IfYouGiveASeedAFertilizer : ISolver
    {
        private string filename;
        private List<long> seeds;
        private List<List<(long destStart, long sourceStart, long rangeLength)>> maps;

        public IfYouGiveASeedAFertilizer(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            this.seeds = lines[0].Split(' ').Skip(1).Select(long.Parse).ToList(); // parse seeds
            this.maps = new List<List<(long destStart, long sourceStart, long rangeLength)>>();
            List<(long destStart, long sourceStart, long rangeLength)>? currmap = null;
            foreach (var line in lines.Skip(2))
            {
                if (line.EndsWith(':'))
                {
                    currmap = new List<(long destStart, long sourceStart, long rangeLength)>();
                    continue;
                }
                else if (line.Length == 0 && currmap != null)
                {
                    maps.Add(currmap!);
                    currmap = null;
                    continue;
                }

                var nums = line.Split(' ').Select(long.Parse).ToArray();
                // [0] == dest start, [1] == source start, [2] == range length
                currmap!.Add((nums[1], nums[1] + nums[2] - 1, nums[0] - nums[1]));
            }
            if (currmap != null)
            {
                maps.Add(currmap);
            }

        }

        public void PartOne()
        {
            var result1 = long.MaxValue;
            foreach (var seed in seeds)
            {
                var value = seed;
                foreach (var map in maps)
                {
                    foreach (var item in map)
                    {
                        if (value >= item.destStart && value <= item.sourceStart)
                        {
                            value += item.rangeLength;
                            break;
                        }
                    }
                }
                result1 = Math.Min(result1, value);
            }
            Console.WriteLine(result1);
        }

        public void PartTwo()
        {

        }
    }
}
