namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Trebuchet : ISolver
    {
        private readonly string filename;
        private string[] data;

        private Dictionary<string, int> numberWords = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
        };

        public Trebuchet(string filename)
        {
            this.filename = filename;
        }

        public void PartOne()
        {
            var ints = new List<int>();
            foreach (var line in this.data)
            {
                var first = line.First(c => char.IsDigit(c));
                var last = line.Last(c => char.IsDigit(c));
                ints.Add(int.Parse($"{first}{last}"));
                Console.WriteLine($"{first}{last}");
            }

            Console.WriteLine($"{ints.Sum()}");
        }

        public void PartTwo()
        {
            var replacements = new Dictionary<string, string>
            {
                { "one", "on1e" },
                { "two", "tw2o" },
                { "three", "thr3e" },
                { "four", "fo4ur" },
                { "five", "fi5ve" },
                { "six", "si6x" },
                { "seven", "sev7en" },
                { "eight", "ei8ght" },
                { "nine", "ni9ne" },
            };

            var sum = 0;
            foreach (var l in this.data)
            {
                StringBuilder sb = new(l);
                foreach(var r in replacements)
                {
                    sb.Replace(r.Key, r.Value);
                }
                var s = sb.ToString();

                var nums = s.ToCharArray().Where(char.IsDigit).ToArray();
                if (int.TryParse($"{nums[0]}{nums[^1]}", out int val))
                {
                    sum += val;
                }
            }

            Console.WriteLine(sum);
        }

        public void Init()
        {
            this.data = File.ReadAllLines(this.filename);
        }
    }
}
