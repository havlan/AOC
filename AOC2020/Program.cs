using AOC2020;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AOC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dayArgs = args.Length > 0 ? int.Parse(args[0]) : 10;
            var month = DateTime.Now.Month;
            const string basePath = @"C:\Users\lahavard\source\repos\AOCData\AOC2020\input";
            var solvers = new Dictionary<int, Func<Task>>()
            {
                { 1,  async () => await One.Solve(basePath + @"\1.txt") },
                { 2,  async () => await Two.Solve(basePath + @"\2.txt") },
                { 3,  async () => await Three.Solve(basePath + @"\3.txt") },
                { 4,  async () => await Four.Solve(basePath + @"\4.txt") },
                { 5,  async () => await Five.Solve(basePath + @"\5.txt") },
                { 6,  async () => await Six.Solve(basePath + @"\6.txt") },
                { 7,  async () => await Seven.Solve(basePath + @"\7.txt") },
                { 8,  async () => await Eight.Solve(basePath + @"\8.txt") },
                { 9,  async () => await Nine.Solve(basePath + @"\9.txt") },
                { 10, async () => await Ten.Solve(basePath + @"\10.txt") },
            };

            if (solvers.TryGetValue(dayArgs, out var action))
            {
                await action();
            }

            Console.WriteLine("Done");
        }
    }
}
