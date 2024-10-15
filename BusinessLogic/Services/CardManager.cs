using BusinessLogic.Exceptions;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public class CardManager
    {
        private readonly IRepository _repository;
        public List<Card> Cards { get; private set; }

        public CardManager(IRepository repository) { 
            _repository = repository;
            Cards = _repository.GetCards();
        }

        public List<Card> GenerateCardList(int pairs)
        {
            if (Cards.Count < pairs) throw new NotEnoughCardsExeption(pairs);

            List<Card> cardPairs = [];

            Cards
                .Take(pairs)
                .ToList()
                .ForEach(x => cardPairs.Add(new Card(x)));

            List<Card> cardDuplicates = [];
            cardPairs.ForEach(x => cardDuplicates.Add(new Card(x)));
            cardPairs.AddRange(cardDuplicates);
            cardPairs = cardPairs.OrderBy(x => new Random().Next()).ToList();

            return cardPairs;
        }

        public (int, int) CalculateGrid(int cardCount)
        {
            int cols = (int)Math.Round(Math.Sqrt(cardCount));
            int rows = (int)Math.Ceiling((double)cardCount / cols);

            return (cols, rows);
        }

        public Card? GetCardFromPosition(string input, List<Card> cards)
        {
            try
            {
                string row = input.Split(",")[0].Trim();
                string col = input.Split(",")[1].Trim();

                int x = int.Parse(col);
                int y = int.Parse(row);

                return cards.FirstOrDefault(card => card.X == x && card.Y == y);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
