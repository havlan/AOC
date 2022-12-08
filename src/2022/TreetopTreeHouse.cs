using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
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
            for (var i = 1; i < this.data.GetLength(0); i++)
            {
                for (var k = 1; k < this.data.GetLength(1); k++)
                {
                    visibleFlag[i, k] = this.checkUp(this.data[i, k], i, k)
                        || this.checkDown(this.data[i, k], i, k)
                        || this.checkLeft(this.data[i, k], i, k)
                        || this.checkRight(this.data[i, k], i, k);
                }
            }

            var alreadyVisible = (2 * this.data.GetLength(0)) + (2 * this.data.GetLength(1)) - 4; // -4 for overlap
            Console.WriteLine("{0}", alreadyVisible + visibleFlag.Count(s => s));
        }

        private bool checkDown(int tree, int row, int column)
        {
            for (int i = (row + 1); i < this.data.Length; i++)
            {
                if (tree <= this.data[row, j])
                {
                    return false;
                }
            }

            return true;
        }


        private bool checkUp(int tree, int row, int column)
        {
            for (int i = (row + 1); i >= 0; i--)
            {
                if (tree <= this.data[row, j])
                {
                    return false;
                }
            }

            return true;
        }

        private bool checkRight(int tree, int row, int column)
        {
            for (int j = (column + 1); j < trees[0].length; j++)
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


        public void PartTwo()
        {
            Console.WriteLine("{0}", 0);
        }

        public void Init()
        {
        }
    }
}