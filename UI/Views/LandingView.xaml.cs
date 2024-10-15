using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class LandingView : Window
    {
        public LandingView()
        {
            InitializeComponent();
            DataContext = new HighscoreViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameView view = new GameView();
            view.Show();
            this.Close();
        }
    }
}