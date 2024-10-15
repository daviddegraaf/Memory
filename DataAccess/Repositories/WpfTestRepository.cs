using BusinessLogic;
using BusinessLogic.Models;

namespace DataAccess.Repositories
{
    public class WpfTestRepository : IRepository
    {
        private static List<Card> _cards = new List<Card>() {
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card1.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card2.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card3.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card4.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card5.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card6.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card7.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card8.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card9.png")},
            {new Card("C:\\Users\\David\\source\\repos\\Aftekenopdracht Memory\\UI\\Assets\\card10.png")}
        };
        private static List<Score> _highscores = new List<Score>()
        {
            { new Score("player1", 100, 4) },
            { new Score("player2", 80, 4) },
            { new Score("player3", 60, 4) },
            { new Score("player4", 40, 4) },
            { new Score("player5", 40, 4) },
            { new Score("player6", 30, 4) },
            { new Score("player7", 20, 4) },
            { new Score("player8", 20, 4) },
            { new Score("player9", 20, 4) },
            { new Score("player10", 10, 4) },
            { new Score("player11", 10, 4) }
        };

        public bool AddHighscore(Score score)
        {
            _highscores.Add(score);
            return true;
        }

        public List<Card> GetCards()
        {
            return _cards;
        }

        public List<Score> GetHighscores()
        {
            return _highscores;
        }

        public bool RemoveHighscore(Score score)
        {
            return _highscores.Remove(score);
        }
    }
}
