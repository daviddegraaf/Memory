
namespace BusinessLogic.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public string Command;

        public CommandNotFoundException(string command) : base($"Command with key {command.ToUpper()} not found. Please try again.") {
            Command = command;
        }
    }
}
