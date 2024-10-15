using BusinessLogic.Models;

namespace BusinessLogic
{
    public interface IRepository
    {
        // Highscore methods
        List<Score> GetHighscores();
        bool AddHighscore(Score score);
        bool RemoveHighscore(Score score);

        // Card methods
        List<Card> GetCards();
    }
}
