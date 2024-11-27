﻿namespace AOC2020
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Ten
    {
        public static async Task Solve(string filename)
        {
            Stopwatch sw1 = Stopwatch.StartNew();
            await TaskOne(filename);
            sw1.Stop();
            Console.WriteLine("PartOne={0}ms", sw1.ElapsedMilliseconds);

            Stopwatch sw2 = Stopwatch.StartNew();
            // await TaskTwo(filename);
            sw2.Stop();
            Console.WriteLine("PartTwo={0}ms", sw2.ElapsedMilliseconds);
        }

        private static async Task TaskTwo(string filename)
        {
            throw new NotImplementedException();
        }

        private static async Task TaskOne(string filename)
        {
            var adapters = await Util<int>.GetDataAsList(filename, int.Parse);
            adapters = adapters.Order().ToList();
            var adapterRating = adapters[^1] + 3;
            var oneDiff = 0;
            var threeDiff = 1;

            for (var i = 0; i < adapters.Count; i++)
            {
                var diff = adapters[i] - (i == 0 ? 0 : adapters[i - 1]);
                if (diff == 1)
                {
                    oneDiff++;
                }
                else if (diff == 3)
                {
                    threeDiff++;
                }
            }

            Console.WriteLine(oneDiff * threeDiff);
        }
    }
}