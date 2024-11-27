using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

public static class Three {
    private static int PartOne((int, int) direction, IList<string> hill){
        (int right, int down) = (direction.Item1, direction.Item2); // start
        int trees = 0, x = 0, y = 0;
        
        while(y < hill.Count){
            if (hill[y][x] == '#'){
                trees++;
            }
            x = (x + right) % hill[0].Length;
            y += down;
        }

        return trees;
    }

    private static long PartTwo((int,int)[] directions, IList<string> hill){
        long allTrees = 1;
        var tasks = new List<Task<int>>();
        foreach(var d in directions){
            tasks.Add(Task.Run(() => PartOne(d, hill)));
        }
        foreach(var t in tasks){
            t.Wait();
            allTrees *= t.Result;
        }
        return allTrees;
    }

    public static async Task Solve(string filename){
        var directions = new[] {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2)
        };
        IList<string> hill = await Util<string>.GetDataAsList(filename, (s) => s);
        Stopwatch sw1 = Stopwatch.StartNew();
        Console.WriteLine(PartOne(directions[1], hill));
        sw1.Stop();
        Console.WriteLine("Day3: PartOne={0}", sw1.ElapsedMilliseconds);
        Stopwatch sw2 = Stopwatch.StartNew();
        Console.WriteLine(PartTwo(directions, hill));
        sw1.Stop();
        Console.WriteLine("Day3: PartTwo={0}", sw2.ElapsedMilliseconds);
    }
}