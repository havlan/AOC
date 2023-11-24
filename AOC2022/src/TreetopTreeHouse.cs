using AOC; using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC_2022
{
    public class TreetopTreeHouse : ISolver
    {
        private record Move(int count, int fromStack, int toStack);
        private readonly string filename;
        private int[,] data;

        public TreetopTreeHouse(string filename)
        {
            this.filename = filename;
            var lines = File.ReadLines(this.filename).ToArray();
            this.data = new int[lines.Length, lines.First().Length];
            for (var i = 0; i < lines.Length; i++)
            {
                for (var k = 0; k < lines[0].Length; k++)
                {
                    this.data[i, k] = int.Parse(lines[i][k].ToString());
                }
            }
        }

        public void PartOne()
        {
            bool[,] visibleFlag = new bool[this.data.GetLength(0), this.data.GetLength(1)];
            for (var i = 1; i < this.data.GetLength(0) - 1; i++)
            {
                for (var k = 1; k < this.data.GetLength(1) - 1; k++)
                {
                    if (this.checkUp(this.data[i, k], i, k) || this.checkDown(this.data[i, k], i, k) || this.checkLeft(this.data[i, k], i, k) || this.checkRight(this.data[i, k], i, k)){
                        //Console.WriteLine("{0}{1} is marked.", i, k);
                        visibleFlag[i,k] = true;
                    }                           
                }
            }

            /*
            for (int i = 0; i < visibleFlag.GetLength(0); i++)
            {
                for (int j = 0; j < visibleFlag.GetLength(1); j++)
                {
                    Console.Write(visibleFlag[i,j] + "\t");
                }
                Console.WriteLine();
            }
            */

            var alreadyVisible = (2 * this.data.GetLength(0)) + (2 * this.data.GetLength(1)) - 4; // -4 for overlap
            var flagsAsEnumerable = from item in visibleFlag.Cast<bool>() select item;
            Console.WriteLine("{0}", alreadyVisible + flagsAsEnumerable.Count(s => s));
        }

        private bool checkDown(int tree, int row, int column)
        {
            for (int i = (row + 1); i < this.data.GetLength(0); i++)
            {
                if (tree <= this.data[i, column])
                {
                    return false;
                }
            }

            return true;
        }


        private bool checkUp(int tree, int row, int column)
        {
            for (int i = (row - 1); i >= 0; i--)
            {
                if (tree <= this.data[i, column])
                {
                    return false;
                }
            }

            return true;
        }

        private bool checkRight(int tree, int row, int column)
        {
            for (int j = (column + 1); j < this.data.GetLength(1); j++)
            {
                if (tree <= this.data[row, j])
                {
                    return false;
                }
            }

            return true;
        }

        private bool checkLeft(int tree, int row, int column)
        {
            for (int j = (column - 1); j >= 0; j--)
            {
                if (tree <= this.data[row, j])
                {
                    return false;
                }
            }

            return true;
        }

        private int getLeftDist(int tree, int row, int column)
        {
            var score = 0;
            for (int j = (column - 1); j >= 0; j--)
            {
                score++;
                if (tree <= this.data[row, j])
                {
                    return score;
                }                
            }

            return score;
        }

        private int getRightDist(int tree, int row, int column)
        {
            var score = 0;
            for (int j = (column + 1); j < this.data.GetLength(1); j++)
            {
                score ++;
                if (tree <= this.data[row, j])
                {
                    return score;
                }
            }

            return score;
        }

        private int getUpDist(int tree, int row, int column)
        {
            var score = 0;
            for (int i = (row - 1); i >= 0; i--)
            {
                score ++;
                if (tree <= this.data[i, column])
                {
                    return score;
                }
            }

            return score;
        }

        private int getDownDist(int tree, int row, int column)
        {
            var score = 0;
            for (int i = (row + 1); i < this.data.GetLength(0); i++)
            {
                score++;
                if (tree <= this.data[i, column])
                {
                    return score;
                }
            }

            return score;
        }


        public void PartTwo()
        {
            int[,] scenicScores = new int[this.data.GetLength(0), this.data.GetLength(1)];
            var currentBest = -1;
            for (var i = 1; i < this.data.GetLength(0) - 1; i++)
            {
                for (var k = 1; k < this.data.GetLength(1) - 1; k++)
                {
                    var dist = new []{ this.getLeftDist(this.data[i, k], i, k), this.getRightDist(this.data[i, k], i, k), this.getUpDist(this.data[i, k], i, k), this.getDownDist(this.data[i, k], i, k) };
                    var currentDist = dist.Aggregate((a,b) => a * b);
                    // Console.WriteLine("Dist:{0} Product:{1}", string.Join(",", dist), currentDist);
                    currentBest = Math.Max(currentBest, currentDist);
                }
            }

            Console.WriteLine("{0}", currentBest);
        }

        public void Init()
        {
        }
    }
}