using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{

    internal class Node
    {
        public string Name { get; }
        public Node Parent { get; }
        public List<Node> Children { get; } = new();
        public List<(string name, int size)> Files { get; } = new();

        public Node(Node parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public Node Up() => Parent;

        public Node Down(string name) => name == "/"
            ? this
            : Children.First(d => d.Name.Equals(name));
            
        public int GetSize() => 
            Children.Any()
                ? Children.Select(c => c.GetSize()).Sum() + Files.Select(f => f.size).Sum()
                : Files.Select(f => f.size).Sum();
    }

    public class NoSpaceLeftOnDevice : ISolver
    {
        private readonly string filename;
        private string[] data;

        public NoSpaceLeftOnDevice(string filename)
        {
            this.filename = filename;
            this.data = File.ReadLines(this.filename).ToArray();
        }

        public void PartOne()
        {
            List<Node> directories = new List<Node>();
            var current = new Node(null, "/");
            directories.Add(current);
            foreach (var line in this.data)
            {
                Console.WriteLine("{0}", line);
                if (line.StartsWith("$ cd"))
                {
                    var part = line.Split(' ')[2];
                    current = part.Equals("..") ? current.Up() : current.Down(part);
                    continue;
                }

                if (line.StartsWith("$ ls"))
                    continue;
                    
                if (line.StartsWith("dir "))
                {
                    var dir = new Node(current, line.Split(" ")[1]);
                    directories.Add(dir);
                    current.Children.Add(dir);
                }
                else
                {
                    var parts = line.Split(" ");
                    current.Files.Add((parts[1], int.Parse(parts[0])));
                }
            }

            var sum = directories
                .Where(d => d.GetSize() <= 100000)
                .Select(d => d.GetSize());
            Console.WriteLine("{0}", string.Join("-", sum));
                // .Sum().ToString();
            
            Console.WriteLine("{0}", sum);
        }


        public void PartTwo()
        {
            var sum = 0;
            
            Console.WriteLine("{0}", sum);
        }

        public void Init()
        {
        }

        private (string, string) getItems(string line)
        {
            var splitIdx = line.Length / 2;
            return (line.Substring(0, splitIdx), line.Substring(splitIdx, splitIdx));
        }
    }
}