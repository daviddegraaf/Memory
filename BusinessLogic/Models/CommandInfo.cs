namespace ConsoleApp
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandKey : Attribute
    {
        public string Key { get; }

        public CommandKey(string key)
        {
            Key = key;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CommandDescription : Attribute
    {
        public string Description { get; }

        public CommandDescription(string description)
        {
            Description = description;
        }
    }

    public class CommandInfo
    {
        public string Command;
        public string CommandDescription;

        public CommandInfo(string command, string commandDescription) { 
            Command = command;
            CommandDescription = commandDescription;
        }
    }
}
