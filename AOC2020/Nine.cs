namespace AOC2020
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class Nine
    {

        public static async Task Solve(string filename)
        {
            Stopwatch sw1 = Stopwatch.StartNew();
            await TaskOne(filename);
            sw1.Stop();
            Console.WriteLine("PartOne={0}ms", sw1.ElapsedMilliseconds);

            Stopwatch sw2 = Stopwatch.StartNew();
            await TaskTwo(filename);
            sw2.Stop();
            Console.WriteLine("PartTwo={0}ms", sw2.ElapsedMilliseconds);
        }

        private static async Task TaskTwo(string filename)
        {
            var lines = await Util<string>.ReadAllLines(filename);
            var data = lines.Select(l => long.Parse(l)).ToArray();
            long target = 20874512;

            for (int i = 0; i < data.Length; i++)
            {
                long sum = data[i];
                for (int j = i + 1; j < data.Length; j++)
                {
                    sum += data[j];
                    if (sum == target)
                    {
                        var range = data.Skip(i).Take(j - i + 1);
                        Console.WriteLine(range.Min() + range.Max());
                        return;
                    }
                    else if (sum > target)
                    {
                        break;
                    }
                }
            }
        }

        private static async Task TaskOne(string filename)
        {
            Dictionary<(long, long), long> sumCache = new Dictionary<(long, long), long>();
            var lines = await Util<string>.ReadAllLines(filename);
            var data = lines.Select(l => long.Parse(l)).ToArray();
            Solve(data, true);
        }

        private static void Solve(long[]data, bool IsPartOne)
        {
            var preamble = 25;
            var firstFailure = -1;
            for (int i = preamble; i < data.Length; i++)
            {
                if (!IsValid(data, i, preamble))
                {
                    if (firstFailure == i - 1)
                    {
                        Console.WriteLine($"{data[i - 1]},{data[i]}");
                        return;
                    }

                    firstFailure = i;

                    if (IsPartOne)
                    {
                        Console.WriteLine(data[i]);
                        return;
                    }
                }
            }
        }

        private static bool IsValid(long[] data, int i, int preamble)
        {
            var element = data[i];

            // check if element has preamble sum
            for (int j = i - preamble; j < i; j++)
            {
                for (int k = i - preamble + 1; k < i; k++)
                {
                    var first = data[j];
                    var second = data[k];

                    if (first + second == element)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
