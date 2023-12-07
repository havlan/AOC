namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WaitForIt : ISolver
    {
        private string filename;
        private List<int> time;
        private List<int> distance;
        private long newTime;
        private long newDist;

        public WaitForIt(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            this.time = lines[0].Split(":")[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            this.distance = lines[1].Split(":")[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            this.newTime = long.Parse(lines[0].Split(":")[1].Trim().Replace(" ", ""));
            this.newDist = long.Parse(lines[1].Split(":")[1].Trim().Replace(" ", ""));
        }

        public void PartOne()
        {
            var allWaysToWin = new List<int>();
            for (var r = 0; r < this.time.Count; r++)
            {
                var waysToWin = 0;
                var firstTime = this.time[r];
                var firstDist = this.distance[r];
                for (var hold = 0; hold < firstTime; hold++)
                {
                    var timeLeft = firstTime - hold;
                    var distTravelled = hold * timeLeft;
                    if (distTravelled > firstDist)
                    {
                        waysToWin++;
                    }
                }

                allWaysToWin.Add(Math.Max(waysToWin, 1));
            }

            allWaysToWin.ForEach(Console.WriteLine);

            Console.WriteLine(allWaysToWin.Aggregate(1, (x, y) => x * y));
        }

        public void PartTwo()
        {
            var b1 = Math.Floor((newTime + Math.Sqrt(Utils.IntPow(newTime, 2) - 4 * newDist)) / 2);
            var b2 = Math.Ceiling((newTime - Math.Sqrt(Utils.IntPow(newTime, 2) - 4 * newDist)) / 2);

            var waysToWin = b1 - b2 + 1;

            Console.WriteLine(waysToWin);
        }
    }
}
