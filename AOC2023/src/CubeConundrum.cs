namespace AOC_2023
{
    using AOC;
    using System.Collections.Generic;

    public class CubeConundrum : ISolver
    {
        private string filename;
        private List<Game> data;

        record Game(int Id, IReadOnlyList<Set> Sets);
        record Set(int Red, int Green, int Blue);

        public CubeConundrum(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            this.data = File.ReadAllLines(this.filename)
            .Select(l =>
            {
                var gameAndSets = l.Split(":");
                return new Game(
                    Id: int.Parse(gameAndSets[0].Split(" ")[1]),
                    Sets: gameAndSets[1].Split(";")
                        .Select(subset =>
                        {
                            var rawSet = subset.Split(",")
                                .Select(v =>
                                {
                                    var colorAndValue = v.Split(" ");
                                    return (Color: colorAndValue[2], Value: int.Parse(colorAndValue[1]));
                                }).ToList();

                            return new Set(
                                Red: rawSet.SingleOrDefault(cv => cv.Color is "red").Value,
                                Green: rawSet.SingleOrDefault(cv => cv.Color is "green").Value,
                                Blue: rawSet.SingleOrDefault(cv => cv.Color is "blue").Value);
                        }).ToList());
            }).ToList();

        }

        public void PartOne()
        {
            var sum = this.data.Where(g => g.Sets.All(sg => sg.Red <= 12 && sg.Green <= 13 && sg.Blue <= 14)).Select(g => g.Id).Sum();
            Console.WriteLine("{0}", sum);
        }

        public void PartTwo()
        {
            var sum = this.data.Select(g => g.Sets.Max(r => r.Red) * g.Sets.Max(r => r.Green) * g.Sets.Max(r => r.Blue)).Sum();
            Console.WriteLine("{0}", sum);
        }
    }
}
