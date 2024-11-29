namespace AOC2020
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Eleven
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
            
        }

        private static async Task TaskOne(string filename)
        {
            
        }
    }
}
