using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Xml.Schema;
using System.Runtime.ConstrainedExecution;
using System.IO;
using AOC.Utils;
using System.Text.RegularExpressions;

namespace AOC
{
    public class ProboscideaVolcanium : ISolver
    {
        internal record Valve(string code, int flow, string[] tunnelLeadsToo)
        {
            public bool isReleased { get; set; }
        }
        private readonly string filename;
        private Dictionary<string, Valve> valves;
        public ProboscideaVolcanium(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            this.valves = new();
            foreach (var line in lines)
            {
                var c = Regex.Matches(line, "([A-Z]{2}|\\d+)").ToList();
                Valve newValve = new(c[0].Value, int.Parse(c[1].Value), c.Skip(2).Select(a => a.Value).ToArray());
                valves.Add(newValve.code, newValve);
            }


            var valveList = valves.Values.OrderBy(a => a.code).Select(a => a.code).ToList();
            var dists = new int[valves.Count, valves.Count];
            for (int i = 0; i < valveList.Count; i++) //Fill in the default values
            {
                for (int j = i; j < valveList.Count; j++)
                {
                    if (i == j) dists[i, j] = 0;
                    else if (valves[valveList[i]].tunnelLeadsToo.Contains(valveList[j]))
                    {
                        dists[i, j] = dists[j, i] = 1;
                    }
                    else
                    {
                        dists[i, j] = dists[j, i] = 9999;
                    }
                }
            }

            for (int k = 0; k < valveList.Count; k++)
            {
                for (int i = 0; i < valveList.Count; i++)
                {
                    for (int j = i + 1; j < valveList.Count; j++)
                    {
                        if (dists[i, k] + dists[k, j] < dists[i, j])
                            dists[i, j] = dists[j, i] = dists[i, k] + dists[k, j];
                    }
                }
            }
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", 0);
        }


        public void PartTwo()
        {
            Console.WriteLine("{0}", 0);
        }
    }
}