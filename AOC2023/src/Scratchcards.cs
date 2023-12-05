using AOC;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AOC_2023
{
    public class Scratchcards : ISolver
    {
        private string filename;
        private string[] data;
        private record Card(int num, HashSet<int> winningNums, HashSet<int> myNums)
        {
            public int GetWinningCount()
            {
                return winningNums.Intersect(myNums).Count();
            }
        }
        private List<Card> game;

        public Scratchcards(string filename)
        {
            this.filename = filename;
        }
        public void Init()
        {
            this.data = File.ReadAllLines(filename);
            this.game = new List<Card>();
            var i = 0;
            foreach (var line in this.data)
            {
                var nums = line.Split(':')[1].Split("|");
                var winningNums = nums[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
                var myNums = nums[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
                this.game.Add(new Card(1, winningNums, myNums));
            }
        }

        public void PartOne()
        {
            var sum = 0;
            foreach(var line in this.data)
            {
                var nums = line.Split(':')[1].Split("|");
                var winningNums = nums[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
                var myNums = nums[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
                var score = winningNums.Intersect(myNums).Count();
                sum += Utils.IntPow(2, (uint)score - 1);
            }

            Console.WriteLine(sum);
        }

        public void PartTwo()
        {
            var allWins = this.game.Select(_ => 1).ToArray();

            for (var i = 0; i < this.game.Count; i++)
            {
                var (card, count) = (this.game[i], allWins[i]);
                var matches = card.GetWinningCount();
                for (var j = 0; j < matches; j++)
                {
                    allWins[i + j + 1] += count;
                }
            }

            Console.WriteLine(allWins.Sum());
        }
    }
}
