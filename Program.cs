using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace AOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = args.Length > 0 ? int.Parse(args[0]) : DateTime.Now.Day;
            var year = DateTime.Now.Year;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var solver = Solve(year, day);
            solver.Init();
            stopWatch.Stop();
            Console.WriteLine("Init finished in {0}ms", stopWatch.ElapsedMilliseconds);
            stopWatch.Restart();
            solver.PartOne();
            Console.WriteLine("Part one inished in {0}ms", stopWatch.ElapsedMilliseconds);
            stopWatch.Restart();
            solver.PartTwo();
            Console.WriteLine("Part two finished in {0}ms", stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
        }

        private static ISolver Solve(int year, int day)
        {
            string directory = Directory.GetCurrentDirectory();
#if DEBUG
            var trimmedDir = directory.Take(directory.IndexOf("bin")); //C: \Users\lahavard\source\repos\AOC\bin\Debug\net6.0\input\2022\1.txt
            directory = string.Concat(trimmedDir);
#endif
            string dataDir = $"\\input\\{year}\\";
            string basePath = string.Format("{0}{1}{2}.txt", directory, dataDir, day);

            if (year == 2022)
            {
                return day switch
                {
                    _ => new CalorieCounting(basePath),
                };
            }

            if (year == 2021)
            {
                return day switch
                {
                    1 => new SonarSweep(basePath),
                    2 => new Dive(basePath),
                    3 => new BinaryDiagnostic(basePath),
                    4 => new GiantSquid(basePath),
                    5 => new HydrothermalVenture(basePath),
                    6 => new Lanternfish(basePath),
                    7 => new TreacheryOfWhales(basePath),
                    8 => new SevenSegmentSearch(basePath),
                    9 => new SmokeBasin(basePath),
                    10 => new SyntaxScoring(basePath),
                    11 => new DumboOctopus(basePath),
                    12 => new PassagePathing(basePath),
                    13 => new TransparentOrigami(basePath),
                    14 => new ExtendedPoymerization(basePath),
                    15 => new Chiton(basePath),
                    16 => new PacketDecoder(basePath),
                    17 => new TrickShot(basePath),
                    _ => throw new ArgumentException($"Day {day} not implemented for year {year}."),
                };
            }

            throw new ArgumentException($"Day {day} not implemented in year {year}.");
        }
    }
}
