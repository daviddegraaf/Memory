using BusinessLogic.Models;
using BusinessLogic.Services;
using System.Collections.ObjectModel;
using WpfApp.Handlers;

namespace WpfApp.ViewModels
{
    public class HighscoreViewModel
    {
        private HighscoreManager _manager;

        // Binding properties
        public ObservableCollection<Score> Highscores { get; private set; }

        public HighscoreViewModel()
        {
            _manager = new HighscoreManager(GameHandler.Repository);
            Highscores = new ObservableCollection<Score>(_manager.Highscores);
        }
    }
}
