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
            string directory = Directory.GetCurrentDirectory();
            string dataDir = "\\input\\";
            string basePath = string.Format("{0}{1}", directory, dataDir);

            var day = args.Length > 0 ? int.Parse(args[0]) : DateTime.Now.Day;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var solver = Solve(day);
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

        private static ISolver Solve(int day)
        {
            string directory = Directory.GetCurrentDirectory();
            string dataDir = "\\input\\";
            string basePath = string.Format("{0}{1}", directory, dataDir);

            return day switch
            {
                1 => new SonarSweep(basePath + "1.txt"),
                2 => new Dive(basePath + "2.txt"),
                3 => new BinaryDiagnostic(basePath + "3.txt"),
                4 => new GiantSquid(basePath + "4.txt"),
                5 => new HydrothermalVenture(basePath + "5.txt"),
                6 => new Lanternfish(basePath + "6.txt"),
                7 => new TreacheryOfWhales(basePath + "7.txt"),
                8 => new SevenSegmentSearch(basePath + "8.txt"),
                9 => new SmokeBasin(basePath + "9.txt"),
                10 => new SyntaxScoring(basePath + "10.txt"),
                11 => new DumboOctopus(basePath + "11.txt"),
                12 => new PassagePathing(basePath + "12.txt"),
                13 => new TransparentOrigami(basePath + "13.txt"),
                14 => new ExtendedPoymerization(basePath + "14.txt"),
                15 => new Chiton(basePath + "15.txt"),
                16 => new PacketDecoder(basePath + "16.txt"),
                17 => new TrickShot(basePath + "17.txt"),
                _ => throw new ArgumentException($"Day {day} not implemented."),
            };
        }
    }
}
