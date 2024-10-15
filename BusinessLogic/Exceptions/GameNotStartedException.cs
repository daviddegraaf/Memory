namespace BusinessLogic.Exceptions
{
    public class GameNotStartedException : Exception
    {
        public GameNotStartedException(string initiator) : base($"Method {initiator} can only be called when the game is started.") { }
    }
}
