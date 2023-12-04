using AOC;

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
        private Card[] game;

        public Scratchcards(string filename)
        {
            this.filename = filename;
        }
        public void Init()
        {
            this.data = File.ReadAllLines(filename);
            this.game = new Card[this.data.Length];
            var i = 0;
            foreach (var line in this.data)
            {
                var nums = line.Split(':')[1].Split("|");
                var winningNums = nums[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
                var myNums = nums[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
                this.game[i] = new Card(1, winningNums, myNums);
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
            foreach(var c in this.game)
            {
                var wins = c.GetWinningCount();
                for(var i=c.num+1;i < c.num + wins + 1; i++)
                {
                    for (int j = 0; j < c.num; j++)
                    {
                        this.game[i].num++;
                    }
                }
            }
        }
    }
}
