using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    class Program
    {
        static void Main(string[] args)
        {

            const string basePath = @"C:\Users\havar\Home\AOC2021\input\";

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