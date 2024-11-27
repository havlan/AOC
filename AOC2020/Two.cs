using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class Two
{

    private static async IAsyncEnumerable<(int, int, char, string)> GetData(string filename)
    {
        using (var fs = new FileStream(filename, FileMode.Open))
        {
            using (var reader = new StreamReader(fs))
            {
                string line;
                int low, high;
                char charToCheck;
                string pw;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var splitString = line.Split('-');
                    low = int.Parse(splitString[0]);
                    line = splitString[1];
                    splitString = line.Split(' ');
                    high = int.Parse(splitString[0]);
                    line = splitString[1] + ' ' + splitString[2];
                    charToCheck = line[0];
                    var lastSplit = line.Split(' ');
                    pw = line.Split(' ')[1];

                    yield return (low, high, charToCheck, pw);
                }
            }
        }
    }

    private static async Task<long> PartOne(string filename)
    {
        long numberOfValidPws = 0;
        await foreach (var tuple in GetData(filename))
        {
            int low = tuple.Item1;
            int high = tuple.Item2;
            char toCheck = tuple.Item3;
            string pw = tuple.Item4;

            int numberOfCharactersDetected = 0;
            foreach (var c in pw)
            {
                if (c == toCheck)
                {
                    numberOfCharactersDetected++;
                }
            }
            if (numberOfCharactersDetected >= low && numberOfCharactersDetected <= high)
            {
                numberOfValidPws++;
            }
        }
        return numberOfValidPws;
    }

    public static async Task<long> PartTwo(string filename)
    {
        long numberOfValidPws = 0;
        await foreach (var tuple in GetData(filename))
        {
            int low = tuple.Item1;
            int high = tuple.Item2;
            char toCheck = tuple.Item3;
            string pw = tuple.Item4;

            bool foundFirst = pw[low - 1] == toCheck;
            bool foundSecond = pw[high - 1] == toCheck;
            if (foundFirst ^ foundSecond)
            {
                numberOfValidPws++;
            }
        }
        return numberOfValidPws;
    }

    public static async Task Solve(string filename)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine(await PartOne(filename));
        sw.Stop();
        Console.WriteLine("PartOne={0}ms", sw.ElapsedMilliseconds);

        Stopwatch stopwatch = Stopwatch.StartNew();
        Console.WriteLine(await PartTwo(filename));
        stopwatch.Stop();
        Console.WriteLine("PartTwo={0}ms", stopwatch.ElapsedMilliseconds);
    }

}