using AOC; using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC_2022
{
    /*
        The device will send your subroutine a datastream buffer (your puzzle input); your subroutine needs to identify the first position where the four most recently received characters were all different. Specifically, it needs to report the number of characters from the beginning of the buffer to the end of the first such four-character marker.
    */
    public class TuningTrouble : ISolver
    {
        private readonly string filename;
        private string data;

        public TuningTrouble(string filename)
        {
            this.filename = filename;
            this.data = File.ReadAllText(this.filename);
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", this.Solve(4));
        }


        public void PartTwo()
        {
            Console.WriteLine("{0}", this.Solve(14));
        }

        public void Init()
        {
        }

        private int Solve(int windowSize)
        {
            var inWindow = new char[windowSize];
            for(var i=0;i<this.data.Length;i++)
            {
                inWindow[i % windowSize] = this.data[i];
                if (i > windowSize - 2 && String.Concat(inWindow).Distinct().Count() == windowSize)
                {
                    return i + 1;
                }
            }

            return 0;
        }
    }
}