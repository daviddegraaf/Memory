namespace BusinessLogic.Exceptions
{
    public class NotEnoughCardsExeption : Exception
    {

        public int MinPairs;

        public NotEnoughCardsExeption(int minPairs)
        {
            MinPairs = minPairs;
        }
    }
}
