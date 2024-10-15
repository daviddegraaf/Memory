namespace BusinessLogic.Exceptions
{
    public class CommandFormatException :Exception
    {
        public string Method;

        public CommandFormatException(string method) : base($"Method {method} does not have a the Command or CommandDescription attribute set.")
        {
            Method = method;
        }
    }
}
