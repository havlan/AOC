using AOC; using System;
using System.Text;
using System.Collections.Immutable;

namespace AOC_2021
{
    class ExtendedPoymerization : ISolver
    {
        private readonly string filename;

        private readonly string[] lines;

        public ExtendedPoymerization(string filename)
        {
            this.filename = filename;
            this.lines = File.ReadAllLines(this.filename);
        }

        public void Init()
        {
        }

        public (string template, ImmutableDictionary<(char, char), char> mapping) ReadData(string[] lines)
        {
            var rules = new Dictionary<(char, char), char>();
            string template = string.Empty;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    continue;
                }

                if (template == "")
                {
                    template = line;
                    continue;
                }


                var rule = line.Split(" -> ");
                var from = rule[0].Trim();
                var to = rule[1].Trim();
                rules.Add((from[0], from[1]), to[0]);
            }

            return (template, rules.ToImmutableDictionary());
        }

        public void PartOne()
        {
            var (template, rules) = ReadData(this.lines);
            var newTemplate = new StringBuilder();
            for (var step = 0; step < 10; step++)
            {
                var nextTemplate = new StringBuilder();
                for (var i = 0; i < template.Length - 1; i++)
                {
                    newTemplate.Append(template[i]);
                    newTemplate.Append(rules[(template[i], template[i + 1])]);
                    newTemplate.Append(template[i + 1]);
                    if (nextTemplate.Length > 0)
                    {
                        nextTemplate.Remove(nextTemplate.Length - 1, 1);
                    }
                    nextTemplate.Append(newTemplate);
                    newTemplate.Clear();
                }

                template = nextTemplate.ToString();
            }

            var (max, min) = CountMaxMinFrequency(template);

            Console.WriteLine("{0}", max - min);
        }

        public void PartTwo()
        {
            var (template, rules) = ReadData(this.lines);
            var pairToFrequency = new Dictionary<(char, char), long>();
            var charFrequency = new Dictionary<char, long>();
            UpdatePairCount(template, pairToFrequency);

            foreach (var c in template)
            {
                charFrequency[c] = charFrequency.ContainsKey(c) ? charFrequency[c] + 1 : 1;
            }

            for (var i = 0; i < 40; i++)
            {
                pairToFrequency = RunStep(pairToFrequency, charFrequency, rules);
            }

            Console.WriteLine("{0}", charFrequency.Values.Max() - charFrequency.Values.Min());
        }

        private (long max, long min) CountMaxMinFrequency(string characters)
        {
            var frequencies = new Dictionary<char, long>();

            foreach (var c in characters)
            {
                if (frequencies.ContainsKey(c))
                {
                    frequencies[c] += 1;
                }
                else
                {
                    frequencies[c] = 1;
                }
            }

            return (frequencies.MaxBy(s => s.Value).Value, frequencies.MinBy(s => s.Value).Value);
        }

        private void UpdatePairCount(string template, Dictionary<(char, char), long> mapping)
        {
            for (int i = 1; i < template.Length; i++)
            {
                var key = (template[i - 1], template[i]);
                mapping[key] = mapping.ContainsKey(key) ? mapping[key] + 1 : 1;
            }
        }

        private Dictionary<(char, char), long> RunStep(Dictionary<(char, char), long> currPairToFreq, Dictionary<char, long> charFrequency, ImmutableDictionary<(char, char), char> rules)
        {
            var stepResult = new Dictionary<(char, char), long>();

            // go though all the current pairs and create a new state dictionary of them
            foreach (var p in currPairToFreq)
            {
                var ruleMapping = rules[p.Key];

                // update the char frequency
                charFrequency[ruleMapping] =
                    charFrequency.ContainsKey(ruleMapping) ?
                    charFrequency[ruleMapping] + p.Value :
                    p.Value;

                var newPair1 = (p.Key.Item1, ruleMapping);
                var newPair2 = (ruleMapping, p.Key.Item2);

                stepResult[newPair1] = stepResult.ContainsKey(newPair1) ? stepResult[newPair1] + p.Value : p.Value;
                stepResult[newPair2] = stepResult.ContainsKey(newPair2) ? stepResult[newPair2] + p.Value : p.Value;
            }

            return stepResult;
        }
    }
}