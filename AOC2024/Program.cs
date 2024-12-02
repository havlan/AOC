namespace AOC_2023
{
    using AOC;
    using System;
    using System.IO;
    using System.Diagnostics;
    using static System.Net.Mime.MediaTypeNames;
    using AOC2024.src;

    class Program
    {
        static void Main(string[] args)
        {
            var dayArgs = args.Length > 0 ? int.Parse(args[0]) : 2;
            var month = DateTime.Now.Month;
            const string basePath = @"C:\Users\lahavard\source\repos\AOCData\AOC2024\";

            var solvers = new Dictionary<int, ISolver>()
            {
                { 1,  new HistorianHysteria(string.Format("{0}\\{1}.txt", basePath, 1)) },
                { 2, new RedNosed(string.Format("{0}\\{1}.txt", basePath, 2)) },
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
