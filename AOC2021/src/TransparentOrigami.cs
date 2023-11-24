using AOC;
using System.Text;

namespace AOC_2021
{
    class TransparentOrigami : ISolver
    {
        private HashSet<(int x, int y)> coordinates;
        private List<(char axis, int point)> folds;
        private string filename;

        public TransparentOrigami(string filename)
        {
            this.filename = filename;
        }
        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            folds = new List<(char, int)>();
            coordinates = new HashSet<(int, int)>();
            bool readCoords = false;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    readCoords = true;
                    continue;
                }
                if (readCoords)
                {
                    var split = line.Split("=");
                    folds.Add((split[0].Last(), int.Parse(split[1])));
                }
                else
                {
                    var split = line.Split(",");
                    coordinates.Add((int.Parse(split[0]), int.Parse(split[1])));
                }
            }
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", FindFolds().First().Count());
        }

        public void PartTwo()
        {
            Console.WriteLine("{0}", PrintCode(FindFolds().Last()));
        }

        IEnumerable<HashSet<(int, int)>> FindFolds()
        {
            foreach (var f in this.folds)
            {
                if (f.axis == 'x')
                {
                    this.coordinates = FoldX(f.point, this.coordinates);
                }
                else if (f.axis == 'y')
                {
                    this.coordinates = FoldY(f.point, this.coordinates);
                }

                yield return this.coordinates;
            }
        }

        string PrintCode(HashSet<(int x, int y)> d)
        {
            var res = new StringBuilder();
            var height = d.MaxBy(p => p.y).y;
            var width = d.MaxBy(p => p.x).x;
            for (var y = 0; y <= height; y++)
            {
                for (var x = 0; x <= width; x++)
                {
                    res.Append(d.Contains((x, y)) ? '#' : ' ');
                }
                res.Append("\n");
            }
            return res.ToString();
        }
        HashSet<(int x, int y)> FoldY(int y, HashSet<(int x, int y)> d) => d.Select(p => p.y > y ? p with { y = 2 * y - p.y } : p).ToHashSet();

        HashSet<(int x, int y)> FoldX(int x, HashSet<(int x, int y)> d) => d.Select(p => p.x > x ? p with { x = 2 * x - p.x } : p).ToHashSet();
    }
}