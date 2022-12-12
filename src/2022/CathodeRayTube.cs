using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Text;

namespace AOC
{
    public class CathodeRayTube : ISolver
    {
        private readonly string filename;
        private List<(string, int)> data;

        public CathodeRayTube(string filename)
        {
            this.filename = filename;
            var lines = File.ReadLines(this.filename);
            this.data = new List<(string, int)>();
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var pair = (split[0], split.Length > 1 ? int.Parse(split[1]) : 0);
                this.data.Add(pair);
            }
        }

        public void PartOne()
        {
            var cycles = data.Select(x => x.Item1 == "noop" ? 1 : 2).Sum();
            var state = 1;
            var sum = 0;
            var idxes = new HashSet<int>()
            {
                20,
                60,
                100,
                140,
                180,
                220
            };
            var instr = 0;
            var hasExecutedOp = false;
            for (var i = 0; i < cycles; i++)
            {
                var instruction = data[instr];
                if (idxes.Contains(i + 1))
                {
                    sum += (i + 1) * state;
                }

                if (instruction.Item1 != "noop")
                {
                    if (hasExecutedOp)
                    {
                        hasExecutedOp = false;
                        state += instruction.Item2;

                    }
                    else
                    {
                        hasExecutedOp = true;
                        continue;
                    }
                }

                instr++;
            }

            Console.WriteLine("Sum={0}", sum);
        }


        public void PartTwo()
        {
            var cycle = 0;
            var register = 1;

            for (var i = 0; i < data.Count; i++)
            {
                if (cycle % 40 == 0)
                {
                    Console.WriteLine();
                }

                if ((cycle + 1) % 40 >= register && cycle % 40 <= register + 1)
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(".");
                }

                cycle++;

                if (data[i].Item1 == "noop")
                {
                }
                else if (data[i].Item1 == "addx")
                {
                    data[i] = ("addy", data[i].Item2);
                    i--;
                }
                else
                {
                    register += data[i].Item2;
                }
            }

            Console.WriteLine();
        }

        public void Init()
        {
        }

        private void DoOneCycle(string currResult)
        {

        }
    }
}