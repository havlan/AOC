using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    /*
        The device will send your subroutine a datastream buffer (your puzzle input); your subroutine needs to identify the first position where the four most recently received characters were all different. Specifically, it needs to report the number of characters from the beginning of the buffer to the end of the first such four-character marker.
    */
    public class RopeBridge : ISolver
    {
        private readonly string filename;
        private List<(char, int)> data;

        public RopeBridge(string filename)
        {
            this.filename = filename;
            var lines = File.ReadAllLines(this.filename);
            data = new List<(char, int)>();

            foreach(var line in lines)
            {
                data.Add((line[0], int.Parse(line.Substring(2))));
            }
        }

        public void PartOne()
        {
            Console.WriteLine("{0}", Snake(2));
        }

        private int Snake(int snakeLength)
        {
            Span<(int x, int y)> snake = new (int x, int y)[snakeLength];
            var positionsVisitedByTail = new HashSet<(int x, int y)> { (0,0) };

            foreach((char direction, int times) move in this.data)
            {
                for (var i = 0; i < move.times; i++)
                {
                    snake[0] = MoveHead(snake[0], move.direction);

                    for (int j = 1; j < snakeLength; j++)
                    {
                        var newTail = moveTailAfterHead(snake[j - 1], snake[j]);

                        if (newTail == snake[j])
                            break;

                        snake[j] = newTail;
                    }


                    positionsVisitedByTail.Add(snake[^1]);
                }
            }

            return positionsVisitedByTail.Count;
        }

        private (int x, int y) MoveHead((int x, int y) head, char dir) =>
		dir switch
		{
			'U' => (head.x, head.y - 1),
			'D' => (head.x, head.y + 1),
			'L' => (head.x - 1, head.y),
			'R' => (head.x + 1, head.y),
			_ => head,
		};
        // woosh, clean
        (int x, int y) moveTailAfterHead((int x, int y) head, (int x, int y) tail) =>
        (head.x - tail.x, head.y - tail.y) switch
        {
            ( > 1, > 1) => (head.x - 1, head.y - 1),
            ( > 1, < -1) => (head.x - 1, head.y + 1),
            ( < -1, > 1) => (head.x + 1, head.y - 1),
            ( < -1, < -1) => (head.x + 1, head.y + 1),
            ( > 1, _) => (head.x - 1, head.y),
            ( < -1, _) => (head.x + 1, head.y),
            (_, > 1) => (head.x, head.y - 1),
            (_, < -1) => (head.x, head.y + 1),
            _ => (tail.x, tail.y),
        };


        public void PartTwo()
        {
            Console.WriteLine("{0}", Snake(10));        
        }

        public void Init()
        {
        }
    }
}