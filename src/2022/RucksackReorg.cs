using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    public class RucksackReorg : ISolver
    {
        private readonly string filename;
        private IEnumerable<string> data;

        public RucksackReorg(string filename)
        {
            this.filename = filename;
            this.data = File.ReadLines(this.filename);
        }

        public void PartOne()
        {
            var sum = 0;
            foreach (var line in this.data)
            {
                var items = getItems(line);
                var firstSet = new HashSet<char>(items.Item1);
                var secondSet = new HashSet<char>(items.Item2);
                var result = firstSet.Intersect(secondSet);
                if (result.Any())
                {
                    var priority = result.First();
                    var priAsint = ((int)priority >= 97) ? (int)priority - 96 : (int)priority - 65 + 27;
                    sum += priAsint;
                }
            }
            Console.WriteLine("{0}", sum);
        }


        public void PartTwo()
        {
            var sum = 0;
            var dataAsArray = this.data.ToArray();
            for (var i = 0; i < dataAsArray.Length; i += 3)
            {
                var firstItem = new HashSet<char>(dataAsArray[i]);
                var secondItem = new HashSet<char>(dataAsArray[i + 1]);
                var thirdItem = new HashSet<char>(dataAsArray[i + 2]);
                var firstIntersect = firstItem.Intersect(secondItem);
                thirdItem.IntersectWith(firstIntersect);
                if (thirdItem.Any())
                {
                    var priority = thirdItem.First();
                    var priAsint = ((int)priority >= 97) ? (int)priority - 96 : (int)priority - 65 + 27;
                    sum += priAsint;
                }
            }
            Console.WriteLine("{0}", sum);
        }

        public void Init()
        {
        }

        private (string, string) getItems(string line)
        {
            var splitIdx = line.Length / 2;
            return (line.Substring(0, splitIdx), line.Substring(splitIdx, splitIdx));
        }
    }
}