using System;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public static class Six
{
    private async static Task<int> PartOne(string filename)
    {
        var data = (await Util<string>.ReadAllText(filename)).Trim();
        var groups = data.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        return groups.Select(s => s.Replace("\r\n", "").Distinct().Count()).Sum();
    }

    private async static Task<int> PartTwo(string filename)
    {
        var data = (await Util<string>.ReadAllText(filename)).Trim();
        var groups = data.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(g => g.Split("\r\n"));
        var result = 0;
        foreach (var g in groups)
        {
            IEnumerable<char> answeredYesByEveryone = null;
            foreach (var ans in g)
            {
                answeredYesByEveryone = answeredYesByEveryone is null ? ans.ToCharArray() : ans.Intersect(answeredYesByEveryone);
            }
            result += answeredYesByEveryone.Count();
        }
        return result;
    }


    public async static Task Solve(string filename)
    {
        Console.WriteLine("Six, part one={0}", await PartOne(filename));
        Console.WriteLine("Six, part two={0}", await PartTwo(filename));
    }
}