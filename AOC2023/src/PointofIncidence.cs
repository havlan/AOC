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

        public PointofIncidence(string filename) 
        {
            this.filename = filename;
        }
        public void Init()
        {
            var allText = File.ReadAllText(this.filename);
            this.puzzles = allText.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void PartOne()
        {
            var sum = 0L;
            foreach (var puzzle in this.puzzles)
            {
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
                        sum += (r + 1) * 100;
                        break;
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
                        sum += column + 1;
                        break;
                    }
                }
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
        }

        public char[] GetColumn(char[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public char[] GetRow(char[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}
