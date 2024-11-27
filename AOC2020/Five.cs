using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Text;
using System.Linq;


public static class Five
{

    private static async Task<int> PartOne(string filename)
    {
        int max = -1;
        await foreach (var seat in Util<string>.GetDataEnumerable(filename, l => l))
        {
            if (seat.Length >= 0)
            {
                var currentSeat = Convert.ToInt32(seat.Replace("F", "0").Replace("L", "0").Replace("R", "1").Replace("B", "1"), 2);
                max = Math.Max(max, currentSeat);
            }
        }
        return max;
    }

    private static async Task<int> PartTwo(string filename)
    {
        List<int> allSeats = new List<int>();
        await foreach (var seat in Util<string>.GetDataEnumerable(filename, l => l))
        {
            if (seat.Length >= 0)
            {
                var currentSeat = Convert.ToInt32(seat.Replace("F", "0").Replace("L", "0").Replace("R", "1").Replace("B", "1"), 2);
                allSeats.Add(currentSeat);
            }
        }
        var max = allSeats.Max();
        var min = allSeats.Min();
        var missingRange = Enumerable.Range(min, max - min);
        var missing = missingRange.Except(allSeats).First();
        return missing;
    }



    public static async Task Solve(string filename)
    {
        Console.WriteLine("Five, part one={0}", await PartOne(filename));
        Console.WriteLine("Five, part two={0}", await PartTwo(filename));
    }
}