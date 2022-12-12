using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System.Collections.Immutable;

namespace AOC
{
    public class MonkeyInTheMiddle : ISolver
    {
        record Monkey(ImmutableList<long> items, Func<long, long> operation, int divisibleBy, int trueToMonkey, int falseToMonkey, long numInspections = 0L)
        {
            public Monkey AfterInspections() => this with { numInspections = numInspections + items.Count, items = ImmutableList<long>.Empty };

            public IEnumerable<(long worry, int target)> DetermineThrows(Func<long, long> reduce)
              => from w in items
                 let wp = operation(w)
                 let reduced = reduce(wp)
                 select (reduced, NextMonkey(reduced));

            public Monkey Catch(IEnumerable<long> moreItems) => this with { items = items.AddRange(moreItems) };

            private int NextMonkey(long worryValue) => worryValue % divisibleBy == 0 ? trueToMonkey : falseToMonkey;
        }
        private readonly string filename;
        private ImmutableArray<Monkey> data;

        public MonkeyInTheMiddle(string filename)
        {
            this.filename = filename;
            var lines = File.ReadLines(this.filename).Where(s => !string.IsNullOrEmpty(s)).ToList();
            this.data = new ImmutableArray<Monkey>();
            var localData = new List<Monkey>();
            foreach (var chunk in lines.Chunk(6))
            {
                localData.Add(ParseMonkey(chunk));
            }

            this.data = localData.ToImmutableArray();
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", CalculateMonkeyBusiness(data, 20, w => w / 3));
        }


        public void PartTwo()
        {
            var modWrap = data.Select(s => s.divisibleBy).Aggregate((a, b) => a * b);
            Console.WriteLine("{0}", CalculateMonkeyBusiness(data, 10_000, w => w % modWrap));
        }

        public void Init()
        {
        }

        long CalculateMonkeyBusiness(ImmutableArray<Monkey> initial, int rounds, Func<long, long> reduce)
        {
            var state = initial;
            for (var i = 0; i < rounds; ++i)
            {
                state = DoOneRound(state, reduce);
            }

            return state.OrderByDescending(m => m.numInspections)
                        .Take(2)
                        .Select(m => m.numInspections)
                        .Aggregate((a, b) => a * b);
        }

        ImmutableArray<Monkey> DoOneRound(ImmutableArray<Monkey> monkeys, Func<long, long> reduce)
        {
            var monkeyBuilder = monkeys.ToBuilder();
            for (var i = 0; i < monkeyBuilder.Count; ++i)
            {
                var monkey = monkeyBuilder[i];

                // where to throw
                var throws = monkey.DetermineThrows(reduce);

                // count inspections, reset items
                monkeyBuilder[i] = monkey.AfterInspections();

                // add items to monkeys again
                foreach (var g in throws.GroupBy(x => x.target))
                {
                    monkeyBuilder[g.Key] = monkeyBuilder[g.Key].Catch(g.Select(x => x.worry));
                }
            }

            return monkeyBuilder.ToImmutableArray();
        }

        private Monkey ParseMonkey(IList<string> lines)
        {
            var startItems = lines[1].Split(':')[1].Split(',').Select(s => long.Parse(s));
            var operation = ParseOperation(lines[2].Split(':', StringSplitOptions.TrimEntries)[1]);
            var divisibleBy = int.Parse(lines[3].Split("divisible by")[1]);
            var trueThrowTo = int.Parse(lines[4].Split("throw to monkey")[1]);
            var falseThrowTo = int.Parse(lines[5].Split("throw to monkey")[1]);
            return new Monkey(startItems.ToImmutableList(), operation, divisibleBy, trueThrowTo, falseThrowTo);
        }

        // NIICE
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