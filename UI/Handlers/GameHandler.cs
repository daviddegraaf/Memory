using BusinessLogic;
using BusinessLogic.Events;
using BusinessLogic.Models;
using DataAccess.Repositories;
using System.Windows;
using System.Windows.Threading;
using WpfApp.Views;

namespace WpfApp.Handlers
{
    public class GameHandler
    {
        private Window _window;
        private Card? _firstCard;
        private Card? _secondCard;

        public static Game? Game;

        public static event EventHandler? CardChangeEvent;

        public static readonly IRepository Repository = new SqliteRepository();

        public GameHandler(Window window)
        {
            _window = window;
        }

        public void SelectCard(Card card)
        {
            if (_firstCard != null && _secondCard != null) return;

            card.IsTurned = true;
            CardChangeEvent?.Invoke(this, EventArgs.Empty);

            if (_firstCard == null)
            {
                _firstCard = card;
            }
            else
            {
                _secondCard = card;
                CardChangeEvent?.Invoke(this, EventArgs.Empty);
                Game!.ValidatePair(_firstCard, _secondCard);
            }
        }

        public void OnStart(object? sender, EventArgs e)
        {

        }

        public void OnStop(object? sender, EventArgs e)
        {
            new GameEndView().Show();
            _window.Close();
        }

        public void OnPairFound(object? sender, PairEventArgs e)
        {
            _firstCard = null;
            _secondCard = null;
        }

        public void OnInvalidPair(object? sender, PairEventArgs e)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            timer.Tick += (s, args) =>
            {
                _firstCard = null;
                _secondCard = null;
                e.FirstCard.IsTurned = false;
                e.SecondCard.IsTurned = false;
                CardChangeEvent?.Invoke(this, EventArgs.Empty);
                timer.Stop();
            };
            timer.Start();
        }

        public void OnAllPairsFound(object? sender, EventArgs e)
        {
            Game!.Stop();
        }
    }
}
