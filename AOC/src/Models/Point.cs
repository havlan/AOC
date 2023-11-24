namespace AOC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public record struct Point(int X, int Y)
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
