using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public class GiantSquid : ISolver
    {
        private record struct BingoCell(int Value, bool Bingo);
        private readonly string filename;
        private List<BingoCell[,]> boards;
        private int[] operations;

        public GiantSquid(string filename)
        {
            this.filename = filename;
            (this.boards, this.operations) = ReadData();
        }

        private (List<BingoCell[,]> bingoBoards, int[] operations) ReadData()
        {
            string[] lines = File.ReadAllLines(this.filename);
            int[] numbers = lines[0].Split(',').Select(x => int.Parse(x)).ToArray();
            List<BingoCell[,]> boards = new();

            int row = 0;

            for (int i = 1; i < lines.Length; ++i)
            {
                string line = lines[i];

                if (line == "")
                {
                    boards.Add(new BingoCell[5, 5]);
                    row = 0;
                }
                else
                {
                    int[] cols = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                    BingoCell[,] board = boards.Last();
                    for (int c = 0; c < cols.Length; ++c)
                        board[c, row] = new BingoCell(cols[c], false);
                    row++;
                }
            }

            return (boards, numbers);
        }

        public void PartOne()
        {
            foreach (var number in this.operations)
            {
                foreach (var b in this.boards)
                {
                    for (int r = 0; r < 5; ++r)
                    {
                        for (int c = 0; c < 5; ++c)
                        {
                            if (b[c, r].Value == number)
                            {
                                b[c, r].Bingo = true;
                            }

                            if (IsBingo(b))
                            {
                                Console.WriteLine("{0}", GetScore(b, number));
                            }
                        }
                    }
                }
            }
            Console.WriteLine("{0}", -1);
        }

        public void PartTwo()
        {
            var boardsWithBingo = new HashSet<BingoCell[,]>();

            foreach (var number in this.operations)
            {
                foreach (var b in this.boards)
                {
                    if (boardsWithBingo.Contains(b))
                    {
                        continue;
                    }

                    for (int r = 0; r < 5; ++r)
                    {
                        for (int c = 0; c < 5; ++c)
                        {
                            if (b[c, r].Value == number)
                            {
                                b[c, r].Bingo = true;
                            }

                            if (IsBingo(b))
                            {
                                boardsWithBingo.Add(b);
                                if (boardsWithBingo.Count == this.boards.Count)
                                {
                                    Console.WriteLine("{0}", GetScore(b, number));
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("{0}", -1);
        }

        private static bool IsBingo(BingoCell[,] board)
        {
            for (int r = 0; r < 5; ++r)
            {
                int bingosInRow = 0;
                int bingosInCol = 0;
                for (int c = 0; c < 5; ++c)
                {
                    if (board[c, r].Bingo)
                        bingosInRow++;
                    if (board[r, c].Bingo)
                        bingosInCol++;
                }

                if (bingosInRow == 5 || bingosInCol == 5)
                    return true;
            }

            return false;
        }

        private static int GetScore(BingoCell[,] board, int number)
        {
            int unmarkedSum = 0;
            for (int r = 0; r < 5; ++r)
            {
                for (int c = 0; c < 5; ++c)
                {
                    if (!board[c, r].Bingo)
                    {
                        unmarkedSum += board[c, r].Value;
                    }
                }
            }

            return unmarkedSum * number;
        }
    }
}
