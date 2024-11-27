using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Seven
{
    public static async Task<int> PartOne(string input)
    {
        var parentsOf = new Dictionary<string, HashSet<string>>();
        await foreach (var line in Util<string>.GetDataEnumerable(input, l => l))
        {
            var descr = ParseLine(line);

            foreach (var (u, bag) in descr.children)
            {
                if (!parentsOf.ContainsKey(bag))
                {
                    parentsOf[bag] = new HashSet<string>();
                }
                parentsOf[bag].Add(descr.bag);
            }
        }

        IEnumerable<string> PathsToRoot(string bag)
        {
            yield return bag;

            if (parentsOf.ContainsKey(bag))
            {
                foreach (var container in parentsOf[bag])
                {
                    foreach (var bagT in PathsToRoot(container))
                    {
                        yield return bagT;
                    }
                }
            }
        }
        return PathsToRoot("shiny gold bag").ToHashSet().Count - 1;
    }

    private static async Task<long> PartTwo(string input)
    {

        var childrenOf = new Dictionary<string, List<(int count, string bag)>>();
        await foreach (var line in Util<string>.GetDataEnumerable(input, l => l))
        {
            if (line.Length > 0)
            {
                var descr = ParseLine(line);
                childrenOf[descr.bag] = descr.children;
            }
        }

        long CountWithChildren(string bag) =>
            1 + childrenOf[bag].Select(s => s.count * CountWithChildren(s.bag)).Sum();

        return CountWithChildren("shiny gold bag") - 1;
    }


    private static (string bag, List<(int count, string bag)> children) ParseLine(string line)
    {
        var bag = Regex.Match(line, "^[a-z]+ [a-z]+ bag").Value;

        var children =
            Regex
                .Matches(line, "(\\d+) ([a-z]+ [a-z]+ bag)")
                .Select(x => (count: int.Parse(x.Groups[1].Value), bag: x.Groups[2].Value))
                .ToList();

        return (bag, children);
    }

    public async static Task Solve(string filename)
    {
        Console.WriteLine("Seven part one={0}", await PartOne(filename));
        var pt2 = await PartTwo(filename);
        Console.WriteLine($"Seven part two={pt2}");
    }
}