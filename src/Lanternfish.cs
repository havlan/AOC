using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public class Lanternfish
    {
        private readonly string filename;
        private List<int> data;

        public Lanternfish(string filename)
        {
            this.filename = filename;
            this.data = ReadData();
        }

        private List<int> ReadData()
        {
            var data = new List<int>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var currentLine = line.Split(",");
                    foreach(var l in currentLine)
                    {
                        data.Add(int.Parse(l));
                    }
                }
            }
            return data;
        }

        public long PartOne()
        {
            return getFishies(80);
        }

        public long PartTwo()
        {
            return getFishies(256);
        }

        private long getFishies(int dayys){
            var fishLifeTickToCountMap = new Dictionary<int, long>();
            var partOneData = data;
            for (var i=0;i<10;i++)
            {
                fishLifeTickToCountMap[i] = 0;
            }

            foreach (var fish in partOneData)
            {
                fishLifeTickToCountMap[fish]++;
            }

            for(var i=0;i<dayys;i++)
            {
                var producedFish = fishLifeTickToCountMap[0];

                // fish is now produced
                fishLifeTickToCountMap[7] += producedFish;
                for (var k = 1; k<fishLifeTickToCountMap.Count; k++){ // do whole length?
                    fishLifeTickToCountMap[k-1] = fishLifeTickToCountMap[k];
                }

                fishLifeTickToCountMap[8] = producedFish;
            }

            return fishLifeTickToCountMap.Sum(s => s.Value);
        }
    }
}