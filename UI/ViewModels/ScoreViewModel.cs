using BusinessLogic.Models;
using BusinessLogic.Services;
using WpfApp.Handlers;

namespace WpfApp.ViewModels
{
    public class ScoreViewModel
    {
        private readonly HighscoreManager _manager;

        // Binding properties
        public Score Score { get; private set; } 
        public bool IsHighscore { get; private set; }
        public string TitleText { get; private set; }
        public string ScoreText { get; private set; }

        public ScoreViewModel()
        {
            _manager = new HighscoreManager(GameHandler.Repository);

            if (GameHandler.Game == null) throw new NullReferenceException("There is no game to show points for.");

            int points = GameHandler.Game.GetPoints();
            int pairs = GameHandler.Game.Cards.FindAll(x => x.IsFound).Count / 2;
            Score = new Score("", points, pairs);
            IsHighscore = _manager.IsHighscore(Score);

            TitleText = IsHighscore ? "You have scored a new highscore!" : "Good job, but still room for improvement.";
            ScoreText = $"You have found {pairs} pairs in {GameHandler.Game.Duration.Minutes}m and {GameHandler.Game.Duration.Seconds}s, and scored {points} points.";
        }

        public void ProcessScore()
        {
            if(IsHighscore)
            {
                _manager.NewHighscore(Score);
            }
        }
    }
}
