namespace AOC_2022
{
    using AOC; using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RegolithReservoir : ISolver
    {
        private readonly string filename;

        public RegolithReservoir(string filename)
        {
            this.filename = filename;
        }

        public void PartOne()
        {
            // (x, y) => IsStone
            var world = new Dictionary<Point, bool>();
            var sandProduction = new Point(500, 0);

            while (true)
            {
                var current = sandProduction;
                
                // if not blocked down, move down

                // check allowed moves

                // if no allowed moves, place sand at rest

                // check if y > maxY => break and count sand
            }
        }

        public void PartTwo()
        {
        }

        public void Init()
        {
        }

        private record struct Point(int X, int Y)
        {
            public IEnumerable<Point> GetAdjacentPoints()
            {
                yield return new(X - 1, Y);
                yield return new(X, Y - 1);
                yield return new(X + 1, Y);
                yield return new(X, Y + 1);
            }

            public IEnumerable<Point> GetSandMovesInOrder()
            {
                yield return new(X - 1, Y - 1);
                yield return new(X + 1, Y - 1);
                yield return new(X + 1, Y);
                yield return new(X, Y + 1);
            }
        }
    }
}
