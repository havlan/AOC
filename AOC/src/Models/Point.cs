namespace AOC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

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

        /// <summary>
        /// X-1 -> X+1, Y-1 -> Y+1
        /// </summary>
        public ISet<Point> GetChebyshevAdjacentSet()
        {
            var set = new HashSet<Point>();

            for (var x = -1; x <= 1; x++)
                for (var y = -1; y <= 1; y++)
                {
                    set.Add(new Point(
                        X: X + x,
                        Y: Y + y));
                }

            set.Remove(item: this);
            return set;
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
