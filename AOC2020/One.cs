using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class One
{
    private static async Task<IList<int>> GetData(string fileName)
    {
        var data = new List<int>();
        using (var fs = new FileStream(fileName, FileMode.Open))
        {
            using (var reader = new StreamReader(fs))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    data.Add(int.Parse(line));
                }
            }
        }
        return data;
    }

    private static async Task<long> TaskOne(string fileName)
    {
        var data = await GetData(fileName);
        foreach (var outer in data)
        {
            foreach (var inner in data)
            {
                if (outer + inner == 2020)
                {
                    return outer * inner;
                }
            }
        }
        return 0;
    }

    private static async Task<long> TaskTwo(string filename)
    {
        var data = await GetData(filename);
        foreach (var x in data)
        {
            foreach (var y in data)
            {
                foreach (var z in data)
                {
                    if (x + y + z == 2020)
                    {
                        return x * y * z;
                    }
                }
            }
        }
        return 0;
    }

    public static async Task Solve(string filename)
    {
        Stopwatch sw1 = Stopwatch.StartNew();
        Console.WriteLine(await TaskOne(filename));
        sw1.Stop();
        Console.WriteLine("PartOne={0}ms", sw1.ElapsedMilliseconds);

        Stopwatch sw2 = Stopwatch.StartNew();
        Console.WriteLine(await TaskTwo(filename));
        sw2.Stop();
        Console.WriteLine("PartTwo={0}ms", sw2.ElapsedMilliseconds);
    }
}