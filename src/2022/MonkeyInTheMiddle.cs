using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Text;

namespace AOC
{
    public class MonkeyInTheMiddle : ISolver
    {
        record Monkey(List<long> items, Func<long, long> operation, int divisibleBy, int trueToMonkey, int falseToMonkey, long numInspections = 0L)
        {
            public Monkey AfterInspections() => this with { numInspections = numInspections + items.Count, items = Array.Empty<long>().ToList() };

            public IEnumerable<(long worry, int target)> DetermineThrows(Func<long, long> reduce)
              => from w in items
                 let wp = operation(w)
                 let reduced = reduce(wp)
                 select (reduced, NextMonkey(reduced));

            public void Catch(IEnumerable<long> moreItems) => items.AddRange(moreItems);

            private int NextMonkey(long worryValue) => worryValue % divisibleBy == 0 ? trueToMonkey : falseToMonkey;
        }
        private readonly string filename;
        private List<Monkey> data;

        public MonkeyInTheMiddle(string filename)
        {
            this.filename = filename;
            var lines = File.ReadLines(this.filename).Where(s => !string.IsNullOrEmpty(s)).ToList();
            this.data = new List<Monkey>();
            var idx = 0;
            for (var i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    data.Add(ParseMonkey(lines.GetRange(idx, 6)));
                    idx = i;
                    continue;
                }
            }
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", CalculateMonkeyBusiness(data, 20, w => w / 3));
        }


        public void PartTwo()
        {
            Console.WriteLine();
        }

        public void Init()
        {
        }

        long CalculateMonkeyBusiness(List<Monkey> initial, int rounds, Func<long, long> reduce)
        {
            var state = initial;
            for (var i = 0; i < rounds; ++i)
                state = DoOneRound(state, reduce);

            return state.OrderByDescending(m => m.numInspections)
                        .Take(2)
                        .Select(m => m.numInspections)
                        .Aggregate((a, b) => a * b);
        }

        List<Monkey> DoOneRound(List<Monkey> monkeys, Func<long, long> reduce)
        {
            for (var i = 0; i < monkeys.Count; ++i)
            {
                var monkey = monkeys[i];
                var throws = monkey.DetermineThrows(reduce);
                monkeys[i] = monkey.AfterInspections();

                foreach (var g in throws.GroupBy(x => x.target))
                {
                    monkeys[g.Key].Catch(g.Select(x => x.worry));
                }
            }

            return monkeys;
        }

        private Monkey ParseMonkey(List<string> lines)
        {
            var startItems = lines[1].Split(':')[1].Split(',').Select(s => long.Parse(s));
            var operation = ParseOperation(lines[2].Split(':', StringSplitOptions.TrimEntries)[1]);
            var divisibleBy = int.Parse(lines[3].Split("divisible by")[1]);
            var trueThrowTo = int.Parse(lines[4].Split("throw to monkey")[1]);
            var falseThrowTo = int.Parse(lines[4].Split("throw to monkey")[1]);
            return new Monkey(startItems.ToList(), operation, divisibleBy, trueThrowTo, falseThrowTo);
        }

        private Func<long, long> ParseOperation(string input)
        {
            var expression = input.Split('=', 2, StringSplitOptions.TrimEntries)[1];
            var parts = expression.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            return (parts[0], parts[1], parts[2]) switch
            {
                ("old", "*", "old") => (old => old * old),
                ("old", "*", var x) when int.TryParse(x, out var num) => (old => old * num),
                ("old", "+", var x) when int.TryParse(x, out var num) => (old => old + num),
                _ => throw new FormatException()
            };
        }
    }
}