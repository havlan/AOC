using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public class SonarSweep : ISolver
    {
        private readonly string filename;
        private List<int> data;

        public SonarSweep(string filename)
        {
            this.filename = filename;
        }

        private List<int> ReadData()
        {
            var data = new List<int>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(int.Parse(line));
                }
            }
            return data;
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", data.Select((value, idx) => new { value, idx }).Count(s => s.idx > 0 && s.value > data[s.idx - 1]));
        }

        public void PartTwo()
        {
            var previousWindow = int.MaxValue;
            var currentWindow = 0;
            var numSlidingIncreased = 0;
            for (var i = 2; i < data.Count; i++)
            {
                currentWindow = data[i - 2] + data[i - 1] + data[i];
                if (currentWindow > previousWindow)
                {
                    numSlidingIncreased++;
                }

                previousWindow = currentWindow;
            }

            Console.WriteLine("{0}", numSlidingIncreased);
        }

        public void Init()
        {
            this.data = ReadData();
        }
    }
}