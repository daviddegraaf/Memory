using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using ConsoleApp.Handlers;

namespace ConsoleApp.Helpers
{
    public class Commands
    {

        [CommandKey("N")]
        [CommandDescription("Start new game")]
        public void StartGame()
        {
            try
            {
                Program.Game = new Game(Program.CardManager!.GenerateCardList(4));
            } catch(NotEnoughCardsExeption) {
                Console.WriteLine("\nNot enough cards found in database");
                return;
            }

            GameHandler handler = new GameHandler();
            Program.Game.GameStartEvent += handler.OnStart;
            Program.Game.GameEndEvent += handler.OnStop;
            Program.Game.FoundPairEvent += handler.OnPairFound;
            Program.Game.InvalidPairEvent += handler.OnInvalidPair;
            Program.Game.FoundAllPairsEvent += handler.OnAllPairsFound;

            Program.Game.Start();
        }

        [CommandKey("R")]
        [CommandDescription("Reset all highscores")]
        public void ResetHighscores()
        {
            Console.WriteLine("\nAre you sure you want to delete all highscores? (Y/N)");

            string confirm = Console.ReadKey(true).KeyChar.ToString();
            if (confirm == "")
            {
                ResetHighscores();
                return;
            }

            if (confirm.ToUpper() == "Y")
            {
                Program.HighscoreManager!.Highscores.Clear();

                Console.WriteLine();
                Drawings.DrawHighscores();
            }
            Program.CommandInput();
        }

        [CommandKey("E")]
        [CommandDescription("Exit the application")]
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
