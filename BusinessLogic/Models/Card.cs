using Newtonsoft.Json;

namespace BusinessLogic.Models
{
    public class Card
    {
        public string? ImageUrl { get; set; }
        public bool IsTurned { get; set; }
        public bool IsFound { get; set; }
        public char? ConsoleLetter { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Card() { }

        public Card(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public Card(char consoleLetter)
        {
            ConsoleLetter = consoleLetter;
        }

        public Card(Card card)
        {
            ImageUrl = card.ImageUrl;
            ConsoleLetter = card.ConsoleLetter;
        }
    }
}
