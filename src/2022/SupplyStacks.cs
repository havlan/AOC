using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    public class SupplyStacks : ISolver
    {
        private record Move(int count, int fromStack, int toStack);
        private readonly string filename;
        private string[] data;
        private string[] second;
        private List<Stack<char>> stackList1;
        private List<Stack<char>> stackList2;

        public SupplyStacks(string filename)
        {
            this.filename = filename;
            this.data = File.ReadLines(this.filename).ToArray();

            var index = Array.FindIndex(this.data, x => x.StartsWith(" 1"));
            var first = this.data.Take(index).ToArray();
            this.second = this.data.Skip(index + 2).ToArray();

            var stackEnum = this.data[index].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            this.stackList1 = new List<Stack<char>>();
            this.stackList2 = new List<Stack<char>>();


            foreach (var s in stackEnum)
            {
                stackList1.Add(new Stack<char>());
                stackList2.Add(new Stack<char>());
            }

            foreach (var row in first.Reverse())
            {
                for (int i = 0; i < stackList1.Count; i++)
                {
                    var pick = row[1 + (i * 4)];
                    if (pick == ' ')
                    {
                        continue;
                    }

                    stackList1[i].Push(pick);
                    stackList2[i].Push(pick);
                }
            }
        }

        public void PartOne()
        {
            // move 1 from 2 to 1
            foreach (var line in this.second)
            {
                var splitLine = line.Split(' ');
                var move = new Move(int.Parse(splitLine[1]), int.Parse(splitLine[3]), int.Parse(splitLine[5]));
                var i = move.count;
                while (i > 0)
                {
                    var pick = stackList1[move.fromStack - 1].Pop();
                    stackList1[move.toStack - 1].Push(pick);
                    i--;
                }
            }

            Console.WriteLine("{0}", String.Concat(this.stackList1.Select(s => s.Peek())));
        }


        public void PartTwo()
        {
            // move 1 from 2 to 1
            foreach (var line in this.second)
            {
                var splitLine = line.Split(' ');
                var move = new Move(int.Parse(splitLine[1]), int.Parse(splitLine[3]), int.Parse(splitLine[5]));
                var tmpStack = new Stack<char>();
                for (var i = 0; i < move.count; i++)
                {
                    var pick = stackList2[move.fromStack - 1].Pop();
                    tmpStack.Push(pick);
                }

                while (tmpStack.Count() > 0)
                {
                    stackList2[move.toStack - 1].Push(tmpStack.Pop());
                }
            }

            Console.WriteLine("{0}", String.Concat(this.stackList2.Select(s => s.Peek())));
        }

        public void Init()
        {
        }
    }
}