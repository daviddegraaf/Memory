
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using DataAccess.Repositories;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Handlers;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private readonly GameHandler _handler;
        private readonly CardViewModel _cardModel;

        public GameView()
        {
            InitializeComponent();
            _cardModel = new CardViewModel();
            _handler = new GameHandler(this);

            DataContext = _cardModel;

            GameHandler.Game = new Game(_cardModel.Cards.ToList());
            GameHandler.Game.GameStartEvent += _handler.OnStart;
            GameHandler.Game.GameEndEvent += _handler.OnStop;
            GameHandler.Game.FoundPairEvent += _handler.OnPairFound;
            GameHandler.Game.InvalidPairEvent += _handler.OnInvalidPair;
            GameHandler.Game.FoundAllPairsEvent += _handler.OnAllPairsFound;
            
            GameHandler.Game.Start();
        }

        private void Card_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (GameHandler.Game == null) throw new GameNotStartedException("Card_MouseLeftButtonUp");

            Image? image = sender as Image;
            if (image == null) throw new NullReferenceException("Failed to cast sender as image.");

            Card? card = image.DataContext as Card;
            if (card == null) throw new NullReferenceException("Selected card does not exist.");

            if (card.IsTurned) return;

            _handler.SelectCard(card);
        }
    }
}
