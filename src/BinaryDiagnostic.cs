using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    public class BinaryDiagnostic : ISolver
    {
        private readonly string filename;
        private List<string> data;

        private int numBits;

        public BinaryDiagnostic(string filename)
        {
            this.filename = filename;
        }

        private List<string> ReadData()
        {
            var data = new List<string>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    numBits = line.Length;
                    data.Add(line);
                }
            }
            return data;
        }

        public void PartOne()
        {
            var (gamma, epsilon) = (new char[data[0].Length], new char[data[0].Length]);

            for (int i = 0; i < data[0].Length; i++)
            {
                var ones = data.Where(x => x[i] == '1').Count();
                var zeroes = data.Count - ones;

                gamma[i] = ones > zeroes ? '1' : '0';
                epsilon[i] = ones > zeroes ? '0' : '1';
            }

            var (gammaNum, epsilonNum) = (Convert.ToInt64(new string(gamma), 2), Convert.ToInt64(new string(epsilon), 2));

            Console.WriteLine("{0}", gammaNum * epsilonNum);
        }


        public void PartTwo()
        {
            var ox = FilterPattern(data, (mostFrequent, leastFrequent, currentBit) => currentBit == (mostFrequent ?? '1'));
            var sc = FilterPattern(data, (mostFrequent, leastFrequent, currentBit) => currentBit == (leastFrequent ?? '0'));

            Console.WriteLine("{0}", ox * sc);
        }

        private int FilterPattern(List<string> pattern, Func<char?, char?, char, bool> filterFunc)
        {
            for (int i = 0; i < pattern[0].Length && pattern.Count > 1; i++)
            {
                var ones = pattern.Where(x => x[i] == '1').Count();
                var zeros = pattern.Count - ones;

                var isSameAmount = ones == zeros;
                var moreOnes = ones > zeros;

                char? mostCommon = isSameAmount ? null : moreOnes ? '1' : '0';
                char? leastCommon = isSameAmount ? null : moreOnes ? '0' : '1';

                pattern = pattern.Where(x => filterFunc(mostCommon, leastCommon, x[i])).ToList();
            }

            return Convert.ToInt32(pattern[0], 2);
        }

        public void Init()
        {
            this.data = ReadData();
        }
    }
}