using AOC;
using System;
using System.Buffers;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AOC_2023
{
    public class GearRatios : ISolver
    {
        private readonly record struct Number(int Value, HashSet<Point> Positions);
        private readonly record struct Schematic(List<Number> Numbers, Dictionary<char, HashSet<Point>> Symbols);
        private string filename;
        private string[] data;
        private int size;
        private Schematic schematic;
        private const char Void = '.';
        private const char Gear = '*';

        public GearRatios(string filename)
        {
            this.filename = filename;
            this.data = File.ReadAllLines(filename);
        }
        public void Init()
        {
            var symbols = new Dictionary<char, HashSet<Point>>();
            var numbers = new List<Number>();

            for (var y = 0; y < this.data.Length; y++)
                for (var x = 0; x < this.data[0].Length; x++)
                {
                    if (this.data[y][x] == Void)
                    {
                        continue;
                    }

                    if (!char.IsDigit(this.data[y][x]))
                    {
                        if (!symbols.ContainsKey(this.data[y][x]))
                        {
                            symbols[this.data[y][x]] = new HashSet<Point>();
                        }
                        symbols[this.data[y][x]].Add(item: new Point(x, y));
                        continue;
                    }

                    var positions = new HashSet<Point> { new(x, y) };
                    var span = 1;

                    while (x + span < this.data[0].Length && char.IsDigit(this.data[y][x + span]))
                    {
                        positions.Add(item: new Point(X: x + span, y));
                        span++;
                    }

                    var value = int.Parse(this.data[y][x..(x + span)]);
                    var number = new Number(value, positions);

                    numbers.Add(number);
                    x += span - 1;
                }

            this.schematic = new Schematic(numbers, symbols);
        }

        public void PartOne()
        {
            var sum = 0;
            var symbolPositions = schematic.Symbols.Values
                .SelectMany(set => set)
                .ToHashSet();

            foreach (var number in schematic.Numbers)
            {
                var neighbouringPointsToNumber = number.Positions
                    .SelectMany(pos => pos.GetChebyshevAdjacentSet())
                    .ToHashSet();

                // if any symbol has a neighbouring point
                if (symbolPositions.Any(neighbouringPointsToNumber.Contains))
                {
                    sum += number.Value;
                }
            }
            Console.WriteLine(sum);
        }

        public void PartTwo()
        {
            var gearPositions = schematic.Symbols[Gear];
            var sum = 0;

            foreach (var pos in gearPositions)
            {
                var adjPos = pos.GetChebyshevAdjacentSet();
                var adjNum = schematic.Numbers
                    .Where(num => num.Positions.Any(adjPos.Contains))
                    .ToList();

                if (adjNum.Count == 2)
                {
                    sum += adjNum[0].Value * adjNum[1].Value;
                }
            }

            Console.WriteLine(sum);
        }
    }
}
