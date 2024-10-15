using BusinessLogic.Events;
using System.Diagnostics;

namespace BusinessLogic.Models
{
    public class Game
    {
        public List<Card> Cards { get; }
        public int Moves { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsPlaying { get; set; }

        public event EventHandler? GameStartEvent;
        public event EventHandler? GameEndEvent;
        public event EventHandler<PairEventArgs>? FoundPairEvent;
        public event EventHandler<PairEventArgs>? InvalidPairEvent;
        public event EventHandler? FoundAllPairsEvent;

        public Game(List<Card> cards) {
            Cards = cards;
        }

        public void Start()
        {
            StartTime = DateTime.Now;
            IsPlaying = true;

            GameStartEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            Duration = DateTime.Now - StartTime;
            IsPlaying = false;

            GameEndEvent?.Invoke(this, EventArgs.Empty);
        }

        public void ValidatePair(Card card1, Card card2)
        {
            Moves++;
            if(card1.ImageUrl == card2.ImageUrl && card1.ConsoleLetter == card2.ConsoleLetter)
            {
                card1.IsFound = true;
                card2.IsFound = true;

                if (Cards.FindAll(x => x.IsFound == false).Count == 0)
                {
                    FoundAllPairsEvent?.Invoke(this, EventArgs.Empty);
                    return;
                }

                FoundPairEvent?.Invoke(this, new PairEventArgs(card1, card2));
            } else
            {
                InvalidPairEvent?.Invoke(this, new PairEventArgs(card1, card2));
            }
        }

        public int GetPoints()
        {
            int pairs = Cards.FindAll(x => x.IsFound).Count / 2;
            double score = Math.Pow(pairs, 2) / ((double)Duration.TotalSeconds * Moves) * 1000;
            return (int)score;
        }
    }
}
