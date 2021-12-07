using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace AOC
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Directory.GetCurrentDirectory();
            string dataDir = "\\input\\";
            string basePath = string.Format("{0}{1}", directory, dataDir);

            var day = args.Length > 0 ? int.Parse(args[0]) : DateTime.Now.Day;

            ISolver[] solvers = {
                new SonarSweep(basePath + "1.txt"),
                new Dive(basePath + "2.txt"),
                new BinaryDiagnostic(basePath + "3.txt"),
                new GiantSquid(basePath + "4.txt"),
                new HydrothermalVenture(basePath + "5.txt"),
                new Lanternfish(basePath + "6.txt")
            };

            if (day <= solvers.Length)
            {
                Console.WriteLine("Solving day {0}", day);
                solvers[day - 1].PartOne();
                solvers[day - 1].PartTwo();
            }
            else
            {
                var r = new Random();
                var idx = r.Next(0, solvers.Length);
                Console.WriteLine("Solving day {0}", idx);


                solvers[idx].PartOne();
                solvers[idx].PartTwo();
            }
        }
    }
}
