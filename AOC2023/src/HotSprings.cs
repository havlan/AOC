namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HotSprings : ISolver
    {
        private string filename;
        private List<SpringGroup> groups;

        record Spring(char Code)
        {
            public bool IsOperational => Code == '.';
            public bool IsDamaged => Code == '#';
            public bool IsUnknown => Code == '?';
        }

        record SpringGroup(List<int> Groups)
        {
            public List<Spring> Springs = new List<Spring>();
        }

        public HotSprings(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            this.groups = new List<SpringGroup>();
            for(var r=0 ;r < lines.Length; r++)
            {
                var split = lines[r].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var ints = split[1].Split(',').Select(int.Parse).ToList();
                SpringGroup group = new SpringGroup(ints);
                for(var c =0;c < split[0].Length; c++)
                {
                    var spring = new Spring(lines[r][c]);
                    group.Springs.Add(spring);
                    Console.WriteLine(spring);
                }

                groups.Add(group);
            }
        }

        public void PartOne()
        {
            var sum = this.groups.Sum(g => CountPermutationsOfSingleGroup(g));
            Console.WriteLine(sum);
        }

        public void PartTwo()
        {
            var sum = this.groups.Select(Unfold).Select(g => CountPermutationsOfSingleGroup(g)).Sum();
            Console.WriteLine(sum);
        }

        private SpringGroup Unfold(SpringGroup group)
        {
            var (conditions, broken) = (group.Springs, group.Groups);
            Spring[] unfoldedConditions = [.. conditions, new Spring('?'), .. conditions, new Spring('?'), .. conditions, new Spring('?'), .. conditions, new Spring('?'), .. conditions];
            int[] unfoldedBroken = [.. broken, .. broken, .. broken, .. broken, .. broken];
            return new SpringGroup(unfoldedBroken.ToList())
            {
                Springs = unfoldedConditions.ToList()
            };
        }



        private long CountPermutationsOfSingleGroup(SpringGroup spring)
        {
            var countArrangements = 0L;
            var (conditions, broken) = (spring.Springs, spring.Groups);

            // We can place stuff on all non-operational springs
            var possibleOffsets = conditions.Select((c, i) => (c, i)).Where(x => !x.c.IsOperational).Select(x => x.i).ToList();

            var work = new List<(int brokenIndex, int offset, long count)>();

            // enqueue all possible offsets for first broken
            EnqueueWork(0, 0, 0, 1);

            while (work.Count > 0)
            {
                var (brokenIndex, offset, count) = work[0]; work.RemoveAt(0);

                var brokenLength = broken[brokenIndex];
                var offsetEnd = offset + brokenLength;

                // check if we can place the broken here
                if (!((offset == 0 || !conditions[offset - 1].IsDamaged)
                    && (offsetEnd == conditions.Count || offsetEnd < conditions.Count && !conditions[offsetEnd].IsDamaged)
                    && !conditions.GetRange(offset, brokenLength).Any(s => s.IsOperational)))
                {
                    continue;
                }

                // did we reach the end?
                if (brokenIndex == broken.Count - 1)
                {
                    // if there are # left, we did not find a solution
                    if (!conditions[offsetEnd..].Any(s => s.IsDamaged))
                    {
                        countArrangements += count;
                    }
                }
                else
                {
                    // find next possible that should fit
                    var nextPossibleIndex = possibleOffsets.BinarySearch(offsetEnd + 1);
                    if (nextPossibleIndex < 0) 
                    {
                        nextPossibleIndex = ~nextPossibleIndex;
                    }

                    // enqueue next possible offset for next brokens
                    EnqueueWork(brokenIndex + 1, offsetEnd, nextPossibleIndex, count);
                }
            }

            return countArrangements;


            void EnqueueWork(int bi, int offsetEnd, int nextPossibleIndex, long count)
            {
                for (var i = nextPossibleIndex; i < possibleOffsets.Count; i++)
                {
                    var nextOffset = possibleOffsets[i];

                    // if we find #/broken, we can't continue
                    if (conditions.GetRange(offsetEnd, nextOffset - offsetEnd).Any(s => s.IsDamaged))
                    {
                        break;
                    }

                    // find duplicates
                    var wi = work.FindIndex(w => w.brokenIndex == bi && w.offset == nextOffset);
                    if (wi >= 0)
                    {
                        work[wi] = work[wi] with { count = work[wi].count + count };
                    }
                    else
                    {
                        work.Add((bi, nextOffset, count));
                    }
                }
            }
        }
    }
}
