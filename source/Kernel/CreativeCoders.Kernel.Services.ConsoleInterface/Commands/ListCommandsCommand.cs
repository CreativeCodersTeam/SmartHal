//using System.Collections.Generic;
//using System.Threading.Tasks;
//using CreativeCoders.Core;
//using JetBrains.Annotations;

//namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
//{
//    [UsedImplicitly]
//    public class ListCommandsCommand : ConsoleCommandBase
//    {
//        private readonly IEnumerable<IConsoleCommand> _commands;

//        public ListCommandsCommand(IEnumerable<IConsoleCommand> commands)
//        {
//            _commands = commands;
//        }

//        public override Task ExecuteAsync(string[] arguments)
//        {
//            Output.WriteLine("List available commands:");
//            _commands.ForEach(x => Output.WriteLine(x.CommandName));

//            return Task.CompletedTask;
//        }

//        public override string CommandName => "list-commands";
//    }
//}