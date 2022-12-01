using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    public class SyntaxScoring : ISolver
    {
        private readonly string filename;
        private string[] data;

        private int numBits;

        public SyntaxScoring(string filename)
        {
            this.filename = filename;
        }

        private string[] ReadData()
        {
            return File.ReadAllLines(this.filename);
        }

        public void PartOne()
        {
            var errors = new List<int>();
            foreach (var line in this.data)
            {
                errors.Add(ValidateLine(line, new Stack<char>()));
            }
            Console.WriteLine("{0}", errors.Sum());
        }

        private static int ValidateLine(string line, Stack<char> stack)
        {
            foreach (var c in line)
            {
                switch (c)
                {
                    case '(' or '[' or '{' or '<':
                        stack.Push(c);
                        break;

                    case ')' when stack.Pop() != '(':
                        return 3;

                    case ']' when stack.Pop() != '[':
                        return 57;

                    case '}' when stack.Pop() != '{':
                        return 1197;

                    case '>' when stack.Pop() != '<':
                        return 25137;
                }
            }
            return 0;
        }


        public void PartTwo()
        {
            var scores = new List<long>();
            foreach (var line in this.data)
            {
                var stk = new Stack<char>();
                var points = ValidateLine(line, stk);
                if (points != 0 || stk.Count == 0)
                {
                    continue;
                }

                long score = 0;

                while (stk.Count > 0)
                {
                    var toComplete = stk.Pop();

                    score = toComplete switch
                    {
                        '(' => (score * 5) + 1,
                        '[' => (score * 5) + 2,
                        '{' => (score * 5) + 3,
                        '<' => (score * 5) + 4,
                        _ => score
                    };
                }

                scores.Add(score);
            }

            scores.Sort();

            Console.WriteLine("{0}", scores[(int)Math.Floor(scores.Count / 2.0)]);
        }

        public void Init()
        {
            this.data = ReadData();
        }
    }
}