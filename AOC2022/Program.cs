namespace AOC_2022
{
    using AOC;
    using System;
    using System.IO;
    using System.Diagnostics;
    using static System.Net.Mime.MediaTypeNames;

    class Program
    {
        static void Main(string[] args)
        {
            var dayArgs = args.Length > 0 ? int.Parse(args[0]) : 0;
            var month = DateTime.Now.Month;
            string directory = Directory.GetCurrentDirectory();

            if (directory.Contains("bin"))
            {
                var trimmedDir = directory.Take(directory.IndexOf("bin"));
                directory = string.Concat(trimmedDir);
            }

            var solvers = new Dictionary<int, ISolver>()
            {
                { 1,  new CalorieCounting(string.Format("{0}\\input\\{1}.txt", directory, 1)) },
                { 2, new RockPaperScissors(string.Format("{0}\\input\\{1}.txt", directory, 2)) },
                { 3, new RucksackReorg(string.Format("{0}\\input\\{1}.txt", directory, 3)) },
                { 4, new CampCleanup(string.Format("{0}\\input\\{1}.txt", directory, 4)) },
                { 5, new SupplyStacks(string.Format("{0}\\input\\{1}.txt", directory, 5)) },
                { 6, new TuningTrouble(string.Format("{0}\\input\\{1}.txt", directory, 6)) },
                { 7, new NoSpaceLeftOnDevice(string.Format("{0}\\input\\{1}.txt", directory, 7)) },
                { 8, new TreetopTreeHouse(string.Format("{0}\\input\\{1}.txt", directory, 8)) },
                { 9, new RopeBridge(string.Format("{0}\\input\\{1}.txt", directory, 9)) },
                { 10, new CathodeRayTube(string.Format("{0}\\input\\{1}.txt", directory, 10)) },
                { 11, new MonkeyInTheMiddle(string.Format("{0}\\input\\{1}.txt", directory, 11)) },
                { 12, new HillClimbingAlgorithm(string.Format("{0}\\input\\{1}.txt", directory, 12)) },
                { 13, new DistressSignal(string.Format("{0}\\input\\{1}.txt", directory, 13)) },
                { 14, new RegolithReservoir(string.Format("{0}\\input\\{1}.txt", directory, 14)) },
                { 15, new BeaconExclusionZone(string.Format("{0}\\input\\{1}.txt", directory, 15)) },
                { 16, new ProboscideaVolcanium(string.Format("{0}\\input\\{1}.txt", directory, 16)) },
            };

            var solversToRun = new List<ISolver>();

            if (month != 12 && dayArgs != 0)
            {
                solversToRun.AddRange(solvers.Values);
            }
            else
            {
                var day = dayArgs == 0 ? DateTime.Now.Day : dayArgs;
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
