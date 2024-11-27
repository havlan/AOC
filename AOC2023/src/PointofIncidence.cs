using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2023
{
    public class PointofIncidence : ISolver
    {
        private string filename;
        private string[] puzzles;
        private Dictionary<int, (int num, bool isRow)> resultTracker;

        public PointofIncidence(string filename) 
        {
            this.filename = filename;
        }
        public void Init()
        {
            var allText = File.ReadAllText(this.filename);
            this.puzzles = allText.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            resultTracker = new();
        }

        public void PartOne()
        {
            long sum = 0L;
            for (int i = 0; i < this.puzzles.Length; i++)
            {
                string block = this.puzzles[i];
                TryFindReflection2(block, i, out long res);
                sum += res;
            }

            Console.WriteLine(sum);
        }

        private string GetColumn(string[] matrix, int columnNumber)
        {
            StringBuilder sb = new();
            for(var line =0; line < matrix.Length; line++)
            {
                sb.Append(matrix[line][columnNumber]);
            }
            return sb.ToString();
        }

        public void PartTwo()
        {
            long sum = 0;
            for (int j = 0; j < puzzles.Length; j++)
            {
                string block = puzzles[j];
                for (int i = 0; i < block.Length; i++)
                {
                    if (!".#".Contains(block[i])) continue;
                    StringBuilder sb = new StringBuilder(block);

                    sb[i] = sb[i] == '.' ? '#' : '.';
                    if (TryFindReflection2(sb.ToString(), j, out var res))
                    {
                        sum += res;
                        break;
                    }

                }
            }

            Console.WriteLine(sum);
        }

        private bool TryFindReflection2(string puzzle, int id, out long result)
        {
            result = 0L;
            var first = puzzle.Split(Environment.NewLine);

            // compare rows
            for (var r = 0; r < first.Length - 1; r++)
            {
                var foundPerfectReflection = true;
                var up = r;
                for (var down = r + 1; down < first.Length && up >= 0; down++)
                {
                    if (first[up] != first[down])
                    {
                        foundPerfectReflection = false;
                        break;
                    }

                    up--;
                }

                if (foundPerfectReflection)
                {
                    if (resultTracker.TryGetValue(id, out (int num, bool isRow) x))
                    {
                        if (x.isRow && x.num == r) continue;
                    }
                    resultTracker[id] = (r, true);
                    result = (r + 1) * 100;
                    return true;
                }
            }

            for (var column = 0; column < first[0].Length - 1; column++)
            {
                var foundPerfectReflection = true;
                var left = column;
                for (var right = column + 1; right < first[0].Length && left >= 0; right++)
                {
                    var leftString = GetColumn(first, left);
                    var rightString = GetColumn(first, right);
                    if (leftString != rightString)
                    {
                        foundPerfectReflection = false;
                        break;
                    }
                    left--;
                }

                if (foundPerfectReflection)
                {
                    if (resultTracker.TryGetValue(id, out (int num, bool isRow) x))
                    {
                        if (!x.isRow && x.num == column) continue;
                    }
                    result = column + 1;
                    resultTracker[id] = (column, false);
                    return true;
                }
            }

            result = 0;
            return false;
        }

        private bool TryFindReflection(string block, int Id, out int result)
        {
            var asRows = block.Split(Environment.NewLine);
            var asColumns = block.SplitIntoColumns().ToList();

            //Check rows
            for (int i = 1; i < asRows.Length; i++)
            {
                if (asRows.Take(i).Reverse().Zip(asRows.Skip(i)).All(x => x.First == x.Second))
                {
                    if (resultTracker.TryGetValue(Id, out (int num, bool isRow) x))
                    {
                        if (x.isRow && x.num == i) continue;
                    }
                    resultTracker[Id] = (i, true);
                    result = i * 100;
                    return true;
                }
            }

            for (int i = 1; i < asColumns.Count; i++)
            {
                if (asColumns.Take(i).Reverse().Zip(asColumns.Skip(i)).All(x => x.First == x.Second))
                {
                    if (resultTracker.TryGetValue(Id, out (int num, bool isRow) x))
                    {
                        if (!x.isRow && x.num == i) continue;

                    }
                    resultTracker[Id] = (i, false);
                    result = i;
                    return true;
                }
            }

            result = 0;
            return false;
        }
    }
}
