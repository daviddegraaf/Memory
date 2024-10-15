using System.Windows;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for GameEndView.xaml
    /// </summary>
    public partial class GameEndView : Window
    {
        private readonly ScoreViewModel _viewModel;

        public GameEndView()
        {
            InitializeComponent();
            _viewModel = new ScoreViewModel();

            DataContext = _viewModel;
        }

        private void PlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.ProcessScore();

                new LandingView().Show();
                this.Close();
            }
        }
    }
}
