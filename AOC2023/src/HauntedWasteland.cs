namespace AOC_2023
{
    using AOC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HauntedWasteland : ISolver
    {
        record Node(string key, string left, string right);
        private string filename;


        public HauntedWasteland(string filename)
        {
            this.filename = filename;
        }

        private string instructions;
        private Dictionary<string, Node> nodes;

        public void Init()
        {
            var lines = File.ReadAllLines(this.filename);
            this.instructions = lines[0];
            this.nodes = new Dictionary<string, Node>();
            foreach(var line in lines.Skip(2))
            {
                var key = line.Split("=")[0].Trim();
                var left = line.Substring(line.IndexOf("(") + 1, 3);
                var right = line.Substring(line.Length - 4, 3);
                this.nodes.Add(key, new Node(key, left, right));
            }
        }

        public void PartOne()
        {
            string current = "AAA";
            var opIndex = 0;
            var steps = 0;
            do
            {
                var operation = this.instructions[opIndex];
                current = operation switch
                {
                    'L' => this.nodes[current].left,
                    'R' => this.nodes[current].right,
                    _ => throw new Exception("Invalid operation")
                };
                
                opIndex = (opIndex + 1) % this.instructions.Length;
                steps++;
            } while (current != "ZZZ");

            Console.WriteLine(steps);
        }

        public void PartTwo()
        {
            var startNodes = this.nodes.Where(n => n.Value.key.EndsWith("A")).Select(n => n.Value.key).ToList();
            var allSteps = startNodes.Select(_ => (long)0).ToList();
            for(var i=0;i< allSteps.Count;i++)
            {
                var current = startNodes[i];
                var opIndex = 0;
                var steps = 0;
                do
                {
                    var operation = this.instructions[opIndex];
                    current = operation switch
                    {
                        'L' => this.nodes[current].left,
                        'R' => this.nodes[current].right,
                        _ => throw new Exception("Invalid operation")
                    };

                    opIndex = (opIndex + 1) % this.instructions.Length;
                    steps++;
                } while (!current.EndsWith('Z'));
                allSteps[i] = steps; 
            }
            Console.WriteLine(allSteps.Aggregate(Utils.LCM));
        }
    }
}
