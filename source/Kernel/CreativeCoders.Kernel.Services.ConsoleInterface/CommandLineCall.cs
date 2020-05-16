using System;
using System.Collections.Generic;
using System.Linq;
using CreativeCoders.Core;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    public class CommandLineCall
    {
        private readonly List<string> _arguments;
        
        public CommandLineCall(string commandText)
        {
            CommandName = string.Empty;
            _arguments = new List<string>();
            Parse(commandText);
        }

        private void Parse(string commandText)
        {
            var parts = commandText.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                return;
            }
            CommandName = parts[0];
            parts.Skip(1).ForEach(part => _arguments.Add(part));
        }

        public string CommandName { get; private set; }

        public string[] Arguments => _arguments.ToArray();
    }
}