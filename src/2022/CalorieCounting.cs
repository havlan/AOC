using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    /*
    This list represents the Calories of the food carried by five Elves:

    The first Elf is carrying food with 1000, 2000, and 3000 Calories, a total of 6000 Calories.
    The second Elf is carrying one food item with 4000 Calories.
    The third Elf is carrying food with 5000 and 6000 Calories, a total of 11000 Calories.
    The fourth Elf is carrying food with 7000, 8000, and 9000 Calories, a total of 24000 Calories.
    The fifth Elf is carrying one food item with 10000 Calories.
    In case the Elves get hungry and need extra snacks, they need to know which Elf to ask: they'd like to know how many Calories are being carried by the Elf carrying the most Calories. In the example above, this is 24000 (carried by the fourth Elf).

    Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
    */
    public class CalorieCounting : ISolver
    {
        private readonly string filename;
        private List<List<int>> data;

        private int numBits;

        public CalorieCounting(string filename)
        {
            this.filename = filename;
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", this.data.Select(s => s.Sum()).Max());
        }


        public void PartTwo()
        {
            var sumTopThree = this.data.Select(s => s.Sum()).OrderByDescending(s => s);
            Console.WriteLine("{0}", sumTopThree.Take(3).Sum());
        }

        public void Init()
        {
            this.data = new List<List<int>>();
            var data = File.ReadLines(this.filename);
            var currentElf = new List<int>();
            foreach(var line in data){
                if (string.IsNullOrEmpty(line)){
                    this.data.Add(currentElf);
                    currentElf = new List<int>();
                }
                else 
                {
                    currentElf.Add(int.Parse(line));
                }
            }
            // add the last one 2
            this.data.Add(currentElf);

            foreach(var elves in this.data){
                Console.WriteLine(string.Join("-", elves));
            }
        }
    }
}