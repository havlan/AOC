namespace AOC_2021
{
    using AOC;
    using System;
    using System.IO;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            var day = args.Length > 0 ? int.Parse(args[0]) : DateTime.Now.Day;
            var month = DateTime.Now.Month;

            string directory = Directory.GetCurrentDirectory();

            if (directory.Contains("bin"))
            {
                var trimmedDir = directory.Take(directory.IndexOf("bin"));
                directory = string.Concat(trimmedDir);
            }

            var solvers = new Dictionary<int, ISolver>()
            {
                { 1,  new SonarSweep(string.Format("{0}\\input\\{1}.txt", directory, 1)) },
                { 2, new Dive(string.Format("{0}\\input\\{1}.txt", directory, 2)) },
                { 3, new BinaryDiagnostic(string.Format("{0}\\input\\{1}.txt", directory, 3)) },
                { 4, new GiantSquid(string.Format("{0}\\input\\{1}.txt", directory, 4)) },
                { 5, new HydrothermalVenture(string.Format("{0}\\input\\{1}.txt", directory, 5)) },
                { 6, new Lanternfish(string.Format("{0}\\input\\{1}.txt", directory, 6)) },
                { 7, new TreacheryOfWhales(string.Format("{0}\\input\\{1}.txt", directory, 7)) },
                { 8, new SevenSegmentSearch(string.Format("{0}\\input\\{1}.txt", directory, 8)) },
                { 9, new SmokeBasin(string.Format("{0}\\input\\{1}.txt", directory, 9)) },
                { 10, new SyntaxScoring(string.Format("{0}\\input\\{1}.txt", directory, 10)) },
                { 11, new DumboOctopus(string.Format("{0}\\input\\{1}.txt", directory, 11)) },
                { 12, new PassagePathing(string.Format("{0}\\input\\{1}.txt", directory, 12)) },
                { 13, new TransparentOrigami(string.Format("{0}\\input\\{1}.txt", directory, 13)) },
                { 14, new ExtendedPoymerization(string.Format("{0}\\input\\{1}.txt", directory, 14)) },
                { 15, new Chiton(string.Format("{0}\\input\\{1}.txt", directory, 15)) },
                { 16, new PacketDecoder(string.Format("{0}\\input\\{1}.txt", directory, 16)) },
                { 17, new TrickShot(string.Format("{0}\\input\\{1}.txt", directory, 17)) },
            };

            var solversToRun = new List<ISolver>();
            
            if (month != 12)
            {
                solversToRun.AddRange(solvers.Values);
            }
            else
            {
                solversToRun.Add(solvers[day]);
            }

            foreach (var solver in solversToRun)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                solver.Init();
                stopWatch.Stop();
                Console.WriteLine("Init for solver {0} finished in {1}ms", solver.GetType().Name, stopWatch.ElapsedMilliseconds);
                stopWatch.Restart();
                solver.PartOne();
                Console.WriteLine("Part one for solver {0} inished in {1}ms", solver.GetType().Name, stopWatch.ElapsedMilliseconds);
                stopWatch.Restart();
                solver.PartTwo();
                Console.WriteLine("Part two for solver {0} finished in {1}ms", solver.GetType().Name, stopWatch.ElapsedMilliseconds);
                stopWatch.Stop();
            }

        }
    }
}
