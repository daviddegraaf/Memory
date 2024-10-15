using BusinessLogic.Exceptions;
using ConsoleApp.Helpers;
using System.Reflection;

namespace ConsoleApp.Handlers
{
    public class CommandHandler
    {

        private readonly Dictionary<CommandInfo, MethodInfo> _methodMapping;
        private readonly Commands _commands;

        public CommandHandler()
        {
            _methodMapping = [];
            _commands = new Commands();

            _mapCommands();
        }

        private void _mapCommands()
        {
            var methods = typeof(Commands).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                var commandKeys = method.GetCustomAttributes<CommandKey>();
                if (commandKeys.Count() != 1) throw new CommandFormatException(method.Name);

                foreach (CommandKey key in commandKeys)
                {
                    if (key.Key == "") throw new CommandFormatException(method.Name);

                    CommandDescription? description = method.GetCustomAttribute<CommandDescription>();
                    if (description == null || description.Description == "") throw new CommandFormatException(method.Name);

                    _methodMapping.Add(new CommandInfo(key.Key, description.Description), method);
                }
            }
        }

        private void _execute(string command)
        {
            var mapping = _methodMapping.FirstOrDefault(x => x.Key.Command == command);
            if (mapping.Key == null) throw new CommandNotFoundException(command);

            MethodInfo method = mapping.Value;
            method.Invoke(_commands, null);
        }

        public void HandleInput()
        {
            if (_methodMapping.Count == 0)
            {
                Console.WriteLine("No commands found.");
                return;
            }

            Console.WriteLine("\nPlease press one of the following characters:");

            foreach (var item in _methodMapping)
            {
                Console.WriteLine($"{item.Key.Command.ToUpper()} = {item.Key.CommandDescription}");
            }

            ConsoleKeyInfo input = Console.ReadKey(true);

            _execute(input.KeyChar.ToString().ToUpper());
        }
    }
}
