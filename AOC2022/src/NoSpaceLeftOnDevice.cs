using AOC; using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC_2022
{

    internal class Node
    {
        public string Name;
        private Node Parent;
        private Dictionary<string, Node> Children;
        private Dictionary<string, int> Files;

        public Node(Node parent, string name)
        {
            Parent = parent;
            Name = name;
            Files = new Dictionary<string, int>();
            Children = new Dictionary<string, Node>();
        }

        public Node ChangeDirectory(string name) => name.Equals("..") ? Parent : Children[name];

        public int GetSize() => Files.Values.Sum() + Children.Values.Select(c => c.GetSize()).Sum();

        public void AddDirectory(Node dir) => Children.Add(dir.Name, dir);

        public void AddFile(string name, int size) => Files.Add(name, size);
    }

    public class NoSpaceLeftOnDevice : ISolver
    {
        private readonly string filename;
        private string[] data;

        private List<Node> directories = new List<Node>();

        public NoSpaceLeftOnDevice(string filename)
        {
            this.filename = filename;
            this.data = File.ReadLines(this.filename).ToArray();
        }

        public void PartOne()
        {
            var current = new Node(null, "/");
            directories.Add(current);

            foreach (var unTrimmed in this.data.Skip(1))
            {
                var line = unTrimmed.Trim();
                if (line.StartsWith("$ cd"))
                {
                    current = current.ChangeDirectory(line.Split(' ')[2]);
                    continue;
                }

                if (line.StartsWith("$ ls"))
                    continue;

                if (line.StartsWith("dir "))
                {
                    var dir = new Node(current, line.Split(" ")[1]);
                    directories.Add(dir);
                    current.AddDirectory(dir);
                }
                else
                {
                    var parts = line.Split(" ");
                    current.AddFile(parts[1], int.Parse(parts[0]));
                }
            }

            var sum = directories
                .Where(d => d.GetSize() <= 100000)
                .Select(d => d.GetSize())
                .Sum().ToString();

            Console.WriteLine("{0}", sum);
        }


        public void PartTwo()
        {
            // calculate unused space using root dir from part 1.
            // total space - used - 
            var needed = 30_000_000 - (70_000_000 - directories.First(d => d.Name.Equals("/")).GetSize());

            Console.WriteLine("{0}", this.directories.Where(s => s.GetSize() > needed).Select(s => s.GetSize()).Min());
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