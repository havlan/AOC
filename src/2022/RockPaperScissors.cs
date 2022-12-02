using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace AOC
{
    /*
    This list represents the Calories of the food carried by five Elves:

    The first Elf is carrying food with 1000, 2000, and 3000 Calories, a total of 6000 Calories.
    The second Elf is carrying one food item with 4000 Calories.
    The third Elf is carrying food with 5000 and 6000 Calories, a total of 11000 Calories.
    The fourth Elf is carrying food with 7000, 8000, and 9000 Calories, a total of 24000 Calories.
    The fifth Elf is carrying one food item with 10000 Calories.
    In case the Elves get hungry and need extra snacks, they need to know which Elf to ask: they'd like to know how many Calories are being carried by the Elf carrying the most Calories. In the example above, this is 24000 (carried by the fourth Elf).

    Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
    */
    internal record GameStrategy
    {
        internal char opponentMove { get; set; }
        internal char myMove { get; set; }
    }

    public class RockPaperScissors : ISolver
    {
        private readonly string filename;
        private List<GameStrategy> data;
        private Dictionary<char, int> scoreMap = new Dictionary<char, int>(){
            {'X', 1},
            {'Y', 2},
            {'Z', 3}
        };

        public RockPaperScissors(string filename)
        {
            this.filename = filename;
            this.data = new List<GameStrategy>();

            var lines = File.ReadLines(this.filename);
            foreach(var line in lines){
                this.data.Add(new GameStrategy(){
                    opponentMove = line[0],
                    myMove = line[2],
            });
            }
        }

        public void PartOne()
        {
            var sum = this.data.Select(s => this.PlayRound(s) + scoreMap[s.myMove]).Sum();
            Console.WriteLine("{0}", sum);
        }


        public void PartTwo()
        {
            // X => loose
            // Y => draw
            // Z => win
            var sum = 0;
            foreach(var game in this.data){
                this.MapToCorrectMove(game);
                sum += this.PlayRound(game) + scoreMap[game.myMove];
            }

            Console.WriteLine("{0}", sum);

        }

        public void Init()
        {
        }

        private void MapToCorrectMove(GameStrategy round){
            
            if (round.myMove == 'X'){
                round.myMove = round.opponentMove switch {
                    'A' => 'Z',
                    'B' => 'X',
                    'C' => 'Y'
                };
            }
            else if (round.myMove == 'Y'){
                round.myMove = (char)(round.opponentMove + 23);
            }
            else if (round.myMove == 'Z'){
                round.myMove = round.opponentMove switch {
                    'A' => 'Y',
                    'B' => 'Z',
                    'C' => 'X'
                };
            }
        }

        private int PlayRound(GameStrategy round){

            if ((round.opponentMove == 'A' && round.myMove == 'X') ||
                (round.opponentMove == 'B' && round.myMove == 'Y') ||
                (round.opponentMove == 'C' && round.myMove == 'Z')) {
                return 3;
            }

            if (
                (round.opponentMove == 'A' && round.myMove == 'Y') ||
                (round.opponentMove == 'B' && round.myMove == 'Z') ||
                (round.opponentMove == 'C' && round.myMove == 'X')) {
                    return 6;
            }

            return 0;
        }
    }
}