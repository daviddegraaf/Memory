using BusinessLogic.Models;

namespace BusinessLogic.Events
{
    public class PairEventArgs
    {
        public Card FirstCard;
        public Card SecondCard;

        public PairEventArgs(Card firstCard, Card secondCard)
        {
            FirstCard = firstCard;
            SecondCard = secondCard;
        }
    }
}
