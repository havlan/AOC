using AOC; using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Resources;
using System.Net.Sockets;
using System.Collections.Immutable;

namespace AOC_2022
{
    public class DistressSignal : ISolver
    {
        private readonly string filename;
        private IEnumerable<Element> data;

        public DistressSignal(string filename)
        {
            this.filename = filename;
            var lines = File.ReadLines(this.filename).Where(s => !string.IsNullOrWhiteSpace(s));
            this.data = lines.Select(Element.FromJson);
        }

        public void PartOne()
        {
            var chunkIdx = 1;
            var inRightOrder = new List<int>();
            foreach(var chunk in this.data.Chunk(2))
            {
                if (chunk.First().CompareTo(chunk.Last()) == -1)
                {
                    inRightOrder.Add(chunkIdx);
                }

                chunkIdx++;
            }
            Console.WriteLine("{0}",inRightOrder.Sum());
        }


        public void PartTwo()
        {
            var dataCopy = new List<Element>(this.data);
            var elem2 = Element.FromJson("[[2]]");
            var elem6 = Element.FromJson("[[6]]");
            dataCopy.Add(elem2);
            dataCopy.Add(elem6);

            dataCopy.Sort();
            Console.WriteLine("{0}", (dataCopy.IndexOf(elem2) + 1) * (dataCopy.IndexOf(elem6) + 1));
        }

        public void Init()
        {
        }
    }

    abstract record Element : IComparable
    {
        // WOOSH from this link https://www.reddit.com/r/adventofcode/comments/zkmyh4/comment/j037c4c/?utm_source=share&utm_medium=web2x&context=3
        public static Element FromJson(string json) => FromJson((JsonElement)JsonSerializer.Deserialize<object>(json)!);

        public static Element FromJson(JsonElement element) => element.ValueKind == JsonValueKind.Array
                ? new List(element.EnumerateArray().Select(FromJson).ToArray())
                : new Number(element.GetInt32());

        public int CompareTo(object? other)
        {
            // set up
            var p1 = this;
            var p2 = other as Element ?? throw new ArgumentException("Not an element");

            // compare if they are numbers
            if (p1 is Number n1 && p2 is Number n2)
            {
                return n1.Value.CompareTo(n2.Value);
            }

            // convert single int to list with one element
            List l1 = p1 is Number n ? new List(new[] { n }) : (List)p1;
            List l2 = p2 is Number m ? new List(new[] { m }) : (List)p2;

            // compare element by element
            for (int i = 0; ; i++)
            {
                var length1 = l1.Values.Length;
                var length2 = l2.Values.Length;

                // if we're at the end
                if (length1 == length2 && length1 == i)
                {
                    return 0;
                }

                if (i < length1 && i < length2)
                {
                    var result = l1.Values[i].CompareTo(l2.Values[i]);
                    if (result != 0)
                    {
                        return result;
                    }

                    continue;
                }

                // If the left list runs out of items first, the inputs are in the right order
                return length1.CompareTo(length2);
            }
        }

        record Number(int Value) : Element;
        record List(Element[] Values) : Element;
    }
}