using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2023
{
    public class MirageMaintenance : ISolver
    {
        private string filename;
        private List<List<long>> nums;
        private string[] allLines;

        public MirageMaintenance(string filename)
        {
            this.filename = filename;
            this.nums = Array.Empty<List<long>>().ToList();
        }
        public void Init()
        {
            var lines = File.ReadAllLines(filename).Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();
            nums = lines.Select(s => s.Select(long.Parse).ToList()).ToList();
            this.allLines = File.ReadAllLines(filename);
        }

        public void PartOne()
        {
            Console.WriteLine(this.Extrapolate());
        }

        public void PartTwo()
        {
            Console.WriteLine(this.Extrapolate(true));
        }

        private long Extrapolate(bool isPartTwo = false)
        {
            long sum = 0;
            foreach (var currentNumbers in this.nums)
            {
                if (isPartTwo)
                {
                    currentNumbers.Reverse();
                }

                var newNums = currentNumbers;
                var diffs = new List<List<long>> { newNums };

                while (newNums.Any(n => n != 0))
                {
                    newNums = new();

                    for (var i = 0; i < diffs[^1].Count - 1; i++)
                    {
                        var diff = diffs[^1][i + 1] - diffs[^1][i];
                        newNums.Add(diff);
                    }

                    diffs.Add(newNums);
                }

                for (var i = diffs.Count - 1; i > 0; i--)
                {
                    diffs[i - 1].Add(diffs[i - 1][^1] + diffs[i][^1]);
                    diffs[i - 1].Insert(0, diffs[i - 1][0] - diffs[i][0]);
                }

                sum += diffs[0][^1];
            }
            return sum;
        }
    }
}
