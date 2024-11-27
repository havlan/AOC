using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Eight
{

    private static async Task<int> PartOne(string filename)
    {
        var data = (await Util<string>.ReadAllText(filename))
            .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Split(" "))
            .Select(p => (p[0], int.Parse(p[1]))) // why does parsing throw?
            .ToArray();
        return Run(data).acc;
    }

    private static (int acc, bool terminated) Run(IList<(string, int)> program)
    {
        var (ip, acc, seen) = (0, 0, new HashSet<int>());
        while (true)
        {
            if (ip >= program.Count)
            {
                return (acc, true);
            }
            else if (seen.Contains(ip))
            {
                return (acc, false);
            }
            else
            {
                seen.Add(ip);
                var stm = program[ip];
                switch (stm.Item1)
                {
                    case "nop": ip++; break;
                    case "acc": ip++; acc += stm.Item2; break;
                    case "jmp": ip += stm.Item2; break;
                };
            }
        }
    }



    public static async Task Solve(string filename)
    {
        //Console.WriteLine("Eight, part one={0}", await PartOne(filename));
    }
}