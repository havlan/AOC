using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Text;
using System.Linq;

/// Stuck on 253 for a long time, adjusted and learnt from this source https://dev.to/rpalo/advent-of-code-2020-solution-megathread-day-4-passport-processing-399b
public static class Four
{

    public class Passport
    {
        HashSet<string> mandatory = new() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        string[] eyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        Dictionary<string, string> fields = new();

        public Passport(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }

        public static Passport ParsePassport(string chunk)
        {
            var keyValuePairs = chunk
                .Replace(Environment.NewLine, " ")
                .TrimEnd()
                .Split(' ')
                .Select(pair => new KeyValuePair<string, string>(pair[..3], pair[4..]));

            return new Passport(new Dictionary<string, string>(keyValuePairs));
        }

        public bool IsValid()
            => mandatory.All(x => fields.ContainsKey(x));

        public bool IsStrictValid()
            => IsValid() && fields.All(f => IsFieldValid(f.Key, f.Value));

        bool IsFieldValid(string key, string value)
            => key switch
            {
                "byr" => IsYearValid(value, 1920, 2002),
                "iyr" => IsYearValid(value, 2010, 2020),
                "eyr" => IsYearValid(value, 2020, 2030),
                "hgt" => IsHeightValid(value),
                "hcl" => IsHairColorValid(value),
                "ecl" => IsEyeColorValid(value),
                "pid" => IsPidValid(value),
                _ => true
            };

        private bool IsPidValid(string value)
            => value.Length == 9
                && long.TryParse(value, out _);

        private bool IsEyeColorValid(string value)
            => eyeColors.Contains(value);

        private bool IsHairColorValid(string value)
            => value[0] == '#'
                && value.Length == 7
                && value
                    .ToLower()
                    .Skip(1)
                    .All(c => c >= '0' && c <= '9' || c >= 'a' && c <= 'z');

        private bool IsHeightValid(string value)
            => int.TryParse(value[..^2], out int iValue)
                && value[^2..] == "cm"
                    ? iValue >= 150 && iValue <= 193
                    : iValue >= 59 && iValue <= 76;

        bool IsYearValid(string value, int min, int max)
            => int.TryParse(value, out int iValue)
                && iValue >= min && iValue <= max;
    }


    private static async Task<int> PartOne(string filename)
    {
        return (await File.ReadAllTextAsync(filename))
                .Split(Environment.NewLine + Environment.NewLine)
                .Where(x => x.Length > 0)
                .Select(Passport.ParsePassport)
                .Count(p => p.IsValid());

    }
    private static async Task<int> PartTwo(string filename)
    {
        return (await File.ReadAllTextAsync(filename))
                .Split(Environment.NewLine + Environment.NewLine)
                .Where(x => x.Length > 0)
                .Select(Passport.ParsePassport)
                .Count(p => p.IsStrictValid());

    }

    public static async Task Solve(string filename)
    {
        Console.WriteLine(await PartOne(filename));
        Console.WriteLine(await PartTwo(filename));
    }
}