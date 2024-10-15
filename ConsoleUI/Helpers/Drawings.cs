using BusinessLogic.Exceptions;
using BusinessLogic.Models;

namespace ConsoleApp.Helpers
{
    public class Drawings
    {
        public static void DrawHighscores()
        {
            List<Score> highscores = Program.HighscoreManager!.Highscores;

            Console.WriteLine("Highscores:");

            int colWidth = 17;
            string border = new string('-', colWidth * 3 + 4);

            Console.WriteLine(border);
            Console.WriteLine($"| {"Points".PadRight(colWidth)}| {"Player name".PadRight(colWidth)}| {"Pairs".PadRight(colWidth - 3)}|");
            Console.WriteLine(border);

            if (highscores.Count == 0) Console.WriteLine($"| {"No highscores found".PadLeft(colWidth * 2).PadRight(colWidth * 3)} |");

            foreach (Score score in highscores)
            {
                Console.WriteLine($"| {score.Points.ToString().PadRight(colWidth)}| {score.PlayerName.PadRight(colWidth)}| {score.Pairs.ToString().PadRight(colWidth - 3)}|");
            }

            Console.WriteLine(border + "\n");
        }

        public static void DrawGrid()
        {
            if (Program.Game == null) throw new GameNotStartedException("DrawGrid");

            List<Card> cards = Program.Game.Cards;
            (int cols, int rows) = Program.CardManager!.CalculateGrid(cards.Count());

            string head = " ";
            for (int i = 1; i <= cols; i++)
            {
                head += $" {i}";
            }

            Console.WriteLine(head);

            for (int i = 1; i <= rows; i++)
            {
                string grid = i.ToString();

                for (int j = 1; j <= cols; j++)
                {
                    int index = (i - 1) * cols + j - 1;

                    if (index < cards.Count)
                    {
                        Card card = cards[index];

                        card.X = j;
                        card.Y = i;

                        if (card.IsTurned)
                        {
                            grid += " " + card.ConsoleLetter;
                        }
                        else
                        {
                            grid += " #";
                        }
                    }
                    else
                    {
                        grid += "  ";
                    }
                }

                Console.WriteLine(grid);
            }
        }
    }
}
