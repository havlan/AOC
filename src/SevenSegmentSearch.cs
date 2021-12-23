using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    public class SevenSegmentSearch : ISolver
    {
        private readonly string filename;
        private List<(string[] signal, string[] output)> data;

        private int numBits;

        public SevenSegmentSearch(string filename)
        {
            this.filename = filename;
        }

        private List<(string[], string[])> ReadData()
        {
            var data = new List<(string[], string[])>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var signalOutputSplit = line.Split("|");
                    data.Add((signalOutputSplit[0].Split(" "), signalOutputSplit[1].Split(" ")));
                }
            }
            return data;
        }

        public void PartOne()
        {
            var numRelevantDigits = 0;
            foreach (var pair in this.data)
            {
                foreach (var o in pair.output)
                {
                    if (o.Length == 2 ||
                    o.Length == 4 ||
                    o.Length == 3 ||
                    o.Length == 7)
                    {
                        numRelevantDigits++;
                    }
                }
            }

            Console.WriteLine("{0}", numRelevantDigits);
        }


        public void PartTwo()
        {
            long sum = 0;
            foreach (var pair in this.data)
            {
                var signalPatternSorted = new List<string>();
                signalPatternSorted.AddRange(pair.signal);
                signalPatternSorted.Sort((a, b) => a.Length.CompareTo(b.Length));
                var valueToCombinationHashSet = new HashSet<char>[10];

                foreach (var sortedSig in signalPatternSorted)
                {
                    // if we have a simple mapping
                    if (SigLengthToNumber(sortedSig.Length) is int number)
                    {
                        var sigHashSet = new HashSet<char>(sortedSig);
                        valueToCombinationHashSet[number] = sigHashSet;
                    }
                    else if (sortedSig.Length == 5)
                    {
                        var sigHashSet = new HashSet<char>(sortedSig);
                        if (valueToCombinationHashSet[1].Count == 2 && valueToCombinationHashSet[1].IsSubsetOf(sigHashSet))
                        {
                            valueToCombinationHashSet[3] = sigHashSet;
                        }
                        else
                        {
                            sigHashSet.ExceptWith(valueToCombinationHashSet[4]);
                            if (sigHashSet.Count == 3)
                            {
                                valueToCombinationHashSet[2] = new HashSet<char>(sortedSig);
                            }
                            else
                            {
                                valueToCombinationHashSet[5] = new HashSet<char>(sortedSig);
                            }
                        }
                    }
                    else if (sortedSig.Length == 6)
                    {
                        var sigHashSet = new HashSet<char>(sortedSig);
                        if (valueToCombinationHashSet[3].Count == 5 && valueToCombinationHashSet[3].IsSubsetOf(sigHashSet))
                        {
                            valueToCombinationHashSet[9] = sigHashSet;
                        }
                        else if (valueToCombinationHashSet[1].Count == 2 && valueToCombinationHashSet[1].IsSubsetOf(sigHashSet))
                        {
                            valueToCombinationHashSet[0] = sigHashSet;
                        }
                        else
                        {
                            valueToCombinationHashSet[6] = sigHashSet;
                        }
                    }
                }

                List<string> decrypted = new();
                foreach (var n in pair.output)
                {
                    HashSet<char> seq = new(n.ToCharArray());
                    for (int i = 0; i < valueToCombinationHashSet.Length; i++)
                    {
                        if (seq.SetEquals(valueToCombinationHashSet[i]))
                        {
                            decrypted.Add(i.ToString());
                            break;
                        }
                    }
                }
                int decryptednumber = Convert.ToInt32(String.Join("", decrypted));
                sum += decryptednumber;
            }

            Console.WriteLine("{0}", sum);
        }

        private int? SigLengthToNumber(int length)
        {
            return length switch
            {
                2 => 1,
                4 => 4,
                3 => 7,
                7 => 8,
                _ => null,
            };
        }

        public void Init()
        {
            this.data = ReadData();
        }
    }
}