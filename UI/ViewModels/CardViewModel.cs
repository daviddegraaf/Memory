using BusinessLogic.Models;
using System.Collections.ObjectModel;
using BusinessLogic.Services;
using WpfApp.Handlers;
using System.Windows.Data;
using System.Windows;

namespace WpfApp.ViewModels
{
    public class CardViewModel
    {
        private readonly CardManager _manager;

        // Binding properties
        public ObservableCollection<Card> Cards { get; private set; }
        public int Columns { get; private set; }

        public CardViewModel()
        {
            _manager = new CardManager(GameHandler.Repository);
            Cards = new ObservableCollection<Card>(_manager.GenerateCardList(8));

            (Columns, _) = _manager.CalculateGrid(Cards.Count);

            GameHandler.CardChangeEvent += OnCardChange;
        }

        public void OnCardChange(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CollectionViewSource.GetDefaultView(Cards).Refresh();
            }, System.Windows.Threading.DispatcherPriority.Background);
        }
    }
}
