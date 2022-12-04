using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    public class CampCleanup : ISolver
    {
        internal record Sections(int startA, int endA, int startB, int endB);
        private readonly string filename;
        private List<Sections> data;

        public CampCleanup(string filename)
        {
            this.filename = filename;
            var lines = File.ReadLines(this.filename);
            this.data = new List<Sections>();
            foreach (var line in lines)
            {
                var splitLine = line.Split(new[] { '-', ',' });
                this.data.Add(
                    new Sections(
                        int.Parse(splitLine[0]),
                        int.Parse(splitLine[1]),
                        int.Parse(splitLine[2]),
                        int.Parse(splitLine[3]))
                        );
            }
        }

        public void PartOne()
        {
            var count = this.data.Count(s => this.doesFullyContain(s));
            Console.WriteLine("{0}", count);
        }


        public void PartTwo()
        {
            var count = this.data.Count(s => this.anyOverlap(s));
            Console.WriteLine("{0}", count);
        }

        public void Init()
        {
        }

        private bool doesFullyContain(Sections section)
        {
            // A contains B
            if (section.startA <= section.startB && section.endA >= section.endB)
            {
                return true;
            }

            // B contains A
            if (section.startB <= section.startA && section.endB >= section.endA)
            {
                return true;
            }

            return false;
        }

        private bool anyOverlap(Sections section)
        {
            if (section.endA >= section.startB && section.startA <= section.endB)
            {
                return true;
            }

            return false;
        }
    }
}