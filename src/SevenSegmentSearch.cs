using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    public class SevenSegmentSearch : ISolver
    {
        private readonly string filename;
        private List<(string[] signal, string[] output)> data;

        private int numBits;

        public SevenSegmentSearch(string filename)
        {
            this.filename = filename;
            this.data = ReadData();
        }

        private List<(string[], string[])> ReadData()
        {
            var data = new List<(string[], string[])>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var signalOutputSplit = line.Split("|");
                    data.Add((signalOutputSplit[0].Split(" "), signalOutputSplit[1].Split(" ")));
                }
            }
            return data;
        }

        public void PartOne()
        {
            var numRelevantDigits = 0;
            foreach (var pair in this.data)
            {
                foreach (var o in pair.output)
                {
                    if (o.Length == 2 ||
                    o.Length == 4 ||
                    o.Length == 3 ||
                    o.Length == 7)
                    {
                        numRelevantDigits++;
                    }
                }
            }

            Console.WriteLine("{0}", numRelevantDigits);
        }


        public void PartTwo()
        {
            long sum = 0;
            Console.WriteLine("{0}", sum);
        }
    }
}