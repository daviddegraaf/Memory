using BusinessLogic;
using BusinessLogic.Models;

namespace DataAccess.Repositories
{
    public class ConsoleTestRepository : IRepository
    {
        private static List<Card> _cards = new List<Card>() {
            {new Card('A')},
            {new Card('B')},
            {new Card('C')},
            {new Card('D')},
            {new Card('E')},
            {new Card('F')},
            {new Card('G')},
            {new Card('H')},
            {new Card('I')},
            {new Card('J')}
        };
        private static List<Score> _highscores = new List<Score>()
        {
            { new Score("player1", 100, 4) },
            { new Score("player2", 80, 4) },
            { new Score("player3", 30, 4) },
            { new Score("player4", 20, 4) },
            { new Score("player5", 20, 4) },
            { new Score("player6", 20, 4) },
            { new Score("player7", 20, 4) },
            { new Score("player8", 20, 4) },
            { new Score("player9", 20, 4) },
            { new Score("player10", 20, 4) },
            { new Score("player11", 20, 4) }
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
