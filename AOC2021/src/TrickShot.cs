using AOC;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC_2021
{
    public class TrickShot : ISolver
    {
        private string filename;
        private string data;

        private int xMin;
        private int xMax;
        private int yMin;
        private int yMax;
        public TrickShot(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            this.data = File.ReadAllText(this.filename);
            var m = Regex.Matches(this.data, "-?[0-9]+").Select(m => int.Parse(m.Value)).ToArray();
            xMin = m[0];
            xMax = m[1];
            yMin = m[2];
            yMax = m[3];
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", this.Solve().Max());
        }

        public void PartTwo()
        {
            Console.WriteLine("{0}", this.Solve().Count());
        }

        // For each vx0, vy0 combination that reaches the target, yield the highest y value of the trajectory:
        IEnumerable<int> Solve()
        {
            var vx0Min = 0;
            var vx0Max = xMax;
            var vy0Min = yMin;
            var vy0Max = -yMin;

            for (var vx0 = vx0Min; vx0 <= vx0Max; vx0++)
            {
                for (var vy0 = vy0Min; vy0 <= vy0Max; vy0++)
                {

                    var (x, y, vx, vy) = (0, 0, vx0, vy0);
                    var maxY = 0;

                    // while possible maxY values
                    while (x <= xMax && y >= yMin)
                    {
                        x += vx;
                        y += vy;
                        vy -= 1;
                        vx = Math.Max(0, vx - 1);
                        maxY = Math.Max(y, maxY);

                        if (x >= xMin && x <= xMax && y >= yMin && y <= yMax)
                        {
                            yield return maxY;
                            break;
                        }
                    }
                }
            }
        }
    }
}