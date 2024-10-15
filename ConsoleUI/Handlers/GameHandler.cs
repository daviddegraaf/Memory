using BusinessLogic.Events;
using BusinessLogic.Models;
using ConsoleApp.Helpers;

namespace ConsoleApp.Handlers
{
    public class GameHandler
    {
        private Card? _firstCard;

        public void OnStart(object? sender, EventArgs e)
        {
            _pickCard();
        }

        public void OnStop(object? sender, EventArgs e)
        {
            int points = Program.Game!.GetPoints();
            Console.WriteLine($"You played a total of {Program.Game!.Duration.Minutes} minutes and {Program.Game!.Duration.Seconds}, and scored {points} points.");

            Score score = new Score("unknown player", points, Program.Game.Cards.FindAll(x => x.IsFound).Count / 2);

            if(Program.HighscoreManager!.IsHighscore(score))
            {
                Console.WriteLine("\nYOU JUST SCORED A NEW HIGHSCORE!");
                Console.WriteLine("\nPlease provide your name and press enter so the score can be saved in the highscore list.");
                string? name = Console.ReadLine();

                if(name != null) score.PlayerName = name;

                Program.HighscoreManager.NewHighscore(score);
            }

            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey(true);
            Program.Main([]);
        }

        public void OnPairFound(object? sender, PairEventArgs e)
        {
            Console.Clear();
            Drawings.DrawGrid();
            Console.WriteLine("\nThe pair you selected a match! Press any key to continue.");
            Console.ReadKey(true);

            _firstCard = null;
            _pickCard();
        }

        public void OnInvalidPair(object? sender, PairEventArgs e)
        {
            Console.Clear();
            Drawings.DrawGrid();
            Console.WriteLine("\nThe pair you selected was no match. Press any key to continue.");
            Console.ReadKey(true);

            _firstCard = null;
            e.FirstCard.IsTurned = false;
            e.SecondCard.IsTurned = false;

            _pickCard();
        }

        public void OnAllPairsFound(object? sender, EventArgs e)
        {
            Console.Clear();
            Drawings.DrawGrid();
            Console.WriteLine($"\nYou have found all pairs in {Program.Game!.Moves} moves!");
            Program.Game!.Stop();
        }

        private void _pickCard(bool clear = true)
        {
            if (clear)
            {
                Console.Clear();
                Drawings.DrawGrid();
                Console.WriteLine("\nYou have made " + Program.Game!.Moves + (Program.Game!.Moves == 1 ? " move" : " moves") + ".");
            }

            if (_firstCard != null) Console.WriteLine($"You have picked the first card at {_firstCard.Y},{_firstCard.X}.");

            Console.WriteLine("\nWhat card would you like to pick" + (_firstCard != null ? " as second card" : " as first card") + "?" + (Program.Game!.Moves == 0 && _firstCard == null ? " Please specify as \"row,column\", for example 1,3" : ""));
            string? position = Console.ReadLine();

            if (position == null || position == "")
            {
                Console.WriteLine("Please specify a position as \"row,column\".");
                _pickCard(false);
                return;
            }

            Card? card = Program.CardManager!.GetCardFromPosition(position, Program.Game!.Cards);
            if (card == null)
            {
                Console.WriteLine("There is no card at this position.");
                _pickCard(false);
                return;
            }

            if (card.IsTurned || card.IsFound)
            {
                Console.WriteLine("The card at this position is already tuned.");
                _pickCard(false);
                return;
            }

            card.IsTurned = true;

            if(_firstCard != null) {
                Program.Game.ValidatePair(_firstCard, card);
            } else
            {
                _firstCard = card;
                _pickCard();
            }
        }
    }
}
