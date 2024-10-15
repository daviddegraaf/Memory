using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public class HighscoreManager
    {
        private readonly IRepository _repository;
        public List<Score> Highscores { get; private set; }

        public HighscoreManager(IRepository repository)
        {
            _repository = repository;
            Highscores = _repository.GetHighscores()
                .OrderByDescending(x => x.Points)
                .Take(10)
                .ToList();
        }

        public bool IsHighscore(Score score)
        {
            if (Highscores.Count < 10) return true;
            return score.Points > Highscores.Last().Points;
        }

        public void NewHighscore(Score score)
        {
            Highscores.Add(score);
            if(Highscores.Count >= 10)
            {
                Score lowestScore = Highscores
                    .OrderByDescending(x => x.Points)
                    .ToList()
                    .Last();
                _repository.RemoveHighscore(lowestScore);
            }
            _repository.AddHighscore(score);
        }
    }
}
