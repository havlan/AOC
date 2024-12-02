namespace AOC2024.src
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RedNosed(string FileName) : ISolver
    {
        private readonly string fileName = FileName;
        private int[][] matrix;
        private List<int> fails = new List<int>();

        public void Init()
        {
            var data = System.IO.File.ReadAllLines(fileName);
            this.matrix = data.Select(data => data.Split(" ", StringSplitOptions.RemoveEmptyEntries)).Select(data => data.Select(int.Parse).ToArray()).ToArray();
        }

        public void PartOne()
        {
            var countSafe = 0;
            for(var i=0; i < matrix.Length; i++)
            {
                var safe = false;
                var inc = matrix[i][1] > matrix[i][0];
                for (var j = 1; j < matrix[i].Length; j++)
                {
                    if(inc && matrix[i][j] <= matrix[i][j - 1])
                    {
                        safe = false;
                        break;
                    }
                    else if (!inc && matrix[i][j] >= matrix[i][j - 1])
                    {
                        safe = false;
                        break;
                    }

                    safe = Math.Abs(matrix[i][j] - matrix[i][j - 1]) <= 3;
                    if (!safe)
                    {
                        break;
                    }
                }

                countSafe += safe ? 1 : 0;
                if (!safe)
                {
                    fails.Add(i);
                }
            }

            Console.WriteLine(countSafe);

        }

        public void PartTwo()
        {
            var count = 0;

            for(var i = 0; i < matrix.Length; i++)
            {
                var current = matrix[i].ToList();
                var diff = GetDiff(current);
                if (IsSafe(diff))
                {
                    count++;
                    continue;
                }

                var isSafe = false;
                for (var k = 0; !isSafe && k < current.Count; k++)
                {
                    var newReport = new List<int>(current);
                    newReport.RemoveAt(k);
                    var newDiff = GetDiff(newReport);
                    isSafe = IsSafe(newDiff);
                }

                if (isSafe)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        private static bool IsSafe(List<int> differences) =>
            differences.All(difference => difference is >= 1 and <= 3) ||
            differences.All(difference => difference is <= -1 and >= -3);

        private static List<int> GetDiff(List<int> ints)
        {
            var diff = new List<int>();
            for (var i = 1; i < ints.Count; i++)
            {
                diff.Add(ints[i] - ints[i - 1]);
            }
            return diff;
        }
    }
}
