namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CamelCards : ISolver
    {
        public static Dictionary<char, int> Labels = new()
        {
            {'A',13 }, 
            {'K',12 }, 
            {'Q',11 }, 
            {'J',10 },
            {'T',9 }, 
            {'9',8 }, 
            {'8',7 }, 
            {'7',6 },
            {'6',5 }, 
            {'5',4 }, 
            {'4',3 }, 
            {'3',2 }, 
            {'2',1 },
        };

        public static Dictionary<char, int> LabelsV2 = new()
        {
            {'A',13 },
            {'K',12 },
            {'Q',11 },
            {'T',10 },
            {'9',9 },
            {'8',8 },
            {'7',7 },
            {'6',6 },
            {'5',5 },
            {'4',4 },
            {'3',3 },
            {'2',2 },
            {'J',1 },
        };

        record CardComparer(Dictionary<char, int> labels, bool isPart1) : IComparer<Cards>
        {
            public int Compare(Cards c1, Cards c2)
            {
                var result = CompareCardType(c1, c2, isPart1);
                if (result != 0)
                {
                    return result;
                }

                var idx = -1;
                int strength;
                do
                {
                    idx++;

                    var h1 = labels[c1.Hand[idx]];
                    var h2 = labels[c2.Hand[idx]];

                    strength = h1 > h2 ? 1 : h1 < h2 ? -1 : 0;

                } while (strength == 0 && idx < c1.Hand.Length - 1);

                return strength;
            }

            private int CompareCardType(Cards c1, Cards c2, bool isPart1 = true) => isPart1
                ? c1.CompareV1 < c2.CompareV1 ? 1 : c1.CompareV1 > c2.CompareV1 ? -1 : 0
                : c1.CompareV2 < c2.CompareV2 ? 1 : c1.CompareV2 > c2.CompareV2 ? -1 : 0;
        }

        record Cards
        {
            public string Hand;

            public int Bet { get; }

            public Cards(string cards, int bet)
            {
                this.Hand = cards;
                this.Bet = bet;
            }

            public int CompareV1 => Hand
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count()).ToList() switch
            {
                { Count: 1 } => 1,
                { Count: 2 } g when g.First().Count() == 4 => 2,
                { Count: 2 } g when g.First().Count() == 3 => 3,
                { Count: 3 } g when g.First().Count() == 3 => 4,
                { Count: 3 } g when g.First().Count() == 2 => 5,
                { Count: 4 } => 6,
                { Count: 5 } => 7,
                _ => throw new InvalidOperationException(),
            };

            public int CompareV2
            {
                get
                {
                    if (!Hand.Contains("J"))
                    {
                        return CompareV1;
                    }
                    return Hand.Count(c => c == 'J') switch
                    {
                        5 => 1, // all J's
                        _ when Hand.GroupBy(c => c).Count() == 2 => 1, // full house
                        2 or 3 when Hand.GroupBy(c => c).Count() == 3 => 2, // FH
                        2 when Hand.GroupBy(c => c).Count() == 4 => 4, // 2j + 1
                        1 when Hand.GroupBy(c => c).Count() == 5 => 6, // 1 pair
                        1 when Hand.GroupBy(c => c).Count() == 4 => 4, // 1 pair
                        1 when Hand.GroupBy(c => c).Count() == 3 =>
                            Hand.GroupBy(c => c).OrderByDescending(g => g.Count()).First().Count() == 3 ? 2 : 3, // 2 pair + J
                    };
                }
            }
        }

        private string filename;
        private string[] lines;
        private List<Cards> cards;

        public CamelCards(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            this.lines = File.ReadAllLines(this.filename);
            this.cards = this.lines.Select(s => s.Split(' ')).Select(s => new Cards(s[0], int.Parse(s[1]))).ToList();
        }

        public void PartOne()
        {
            var localGames = this.cards;
            localGames.Sort(new CardComparer(Labels, true));
            var score = localGames.Select((c, idx) => c.Bet * (idx + 1)).Sum().ToString();
            Console.WriteLine(score);
        }

        public void PartTwo()
        {
            var localGames = this.cards;
            localGames.Sort(new CardComparer(LabelsV2, false));
            var score = localGames.Select((c, idx) => c.Bet * (idx + 1)).Sum().ToString();
            Console.WriteLine(score);
        }
    }
}
