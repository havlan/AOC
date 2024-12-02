namespace AOC2024.src
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HistorianHysteria(string FileName) : ISolver
    {
        private readonly string fileName = FileName;
        private List<int> L1 = new List<int>();
        private List<int> L2 = new List<int>();

        public void Init()
        {
            var lines = System.IO.File.ReadAllLines(fileName);
            var numbers = lines.Select(s => s.Split("   ", StringSplitOptions.TrimEntries)).Select(lines => lines.Select(int.Parse).ToList()).ToList();
            foreach (var number in numbers)
            {
                L1.Add(number[0]);
                L2.Add(number[1]);
            }

            L1.Sort();
            L2.Sort();
        }

        public void PartOne()
        {
            long diff = 0;
            for (int i = 0; i < L1.Count; i++)
            {
                diff += Math.Abs(L1[i] - L2[i]);
            }

            Console.WriteLine(diff);
        }

        public void PartTwo()
        {
            var l2Freq = new Dictionary<int, int>();
            for (int i = 0; i < L2.Count; i++)
            {
                if (l2Freq.ContainsKey(L2[i]))
                {
                    l2Freq[L2[i]]++;
                }
                else
                {
                    l2Freq[L2[i]] = 1;
                }
            }

            long score = 0;
            foreach (var num in L1)
            {
                if (l2Freq.ContainsKey(num))
                {
                    score += num * l2Freq[num];
                }
            }

            Console.WriteLine(score);
        }
    }
}
