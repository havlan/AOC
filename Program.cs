using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace AOC
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Directory.GetCurrentDirectory();
            string dataDir = "\\input\\";
            string basePath = string.Format("{0}{1}", directory, dataDir);

            var day = args.Length > 0 ? int.Parse(args[0]) : DateTime.Now.Day;

            switch (day)
            {
                case 1:
                    {
                        var dayOne = new SonarSweep(basePath + "1.txt");

                        Console.WriteLine("{0}", dayOne.PartOne());
                        Console.WriteLine("{0}", dayOne.PartTwo());
                        break;
                    }

                case 2:
                    {
                        var dayTwo = new Dive(basePath + "2.txt");
                        Console.WriteLine("{0}", dayTwo.PartOne());
                        Console.WriteLine("{0}", dayTwo.PartTwo());
                        break;
                    }

                case 3:
                    {
                        var dayThree = new BinaryDiagnostic(basePath + "3.txt");
                        Console.WriteLine("{0}", dayThree.PartOne());
                        Console.WriteLine("{0}", dayThree.PartTwo());
                        break;
                    }
                case 4:
                    {
                        var dayFour = new GiantSquid(basePath + "4.txt");
                        Console.WriteLine("{0}", dayFour.PartOne());
                        Console.WriteLine("{0}", dayFour.PartTwo());
                        break;
                    }
                case 5:
                    {
                        var dayFive = new HydrothermalVenture(basePath + "5.txt");
                        Console.WriteLine("{0}", dayFive.PartOne());
                        Console.WriteLine("{0}", dayFive.PartTwo());
                        break;
                    }
                case 6:
                    {
                        var daySix = new Lanternfish(basePath + "6.txt");
                        Console.WriteLine("{0}", daySix.PartOne());
                        Console.WriteLine("{0}", daySix.PartTwo());
                        break;
                    }

                default:
                    {
                        var dayOne = new SonarSweep(basePath + "1.txt");

                        Console.WriteLine("{0}", dayOne.PartOne());
                        Console.WriteLine("{0}", dayOne.PartTwo());
                        break;
                    }
            }
        }
    }
}
