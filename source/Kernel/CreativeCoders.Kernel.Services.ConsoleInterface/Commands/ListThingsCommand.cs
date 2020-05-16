using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class ListThingsCommand : ConsoleCommandBase
    {
        private readonly IThingRepository _thingRepository;

        public ListThingsCommand(IThingRepository thingRepository)
        {
            _thingRepository = thingRepository;
        }
        public override Task ExecuteAsync(string[] arguments)
        {
            var argument = arguments.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(argument))
            {
                ListThings(_thingRepository.ToArray());
            
                return Task.CompletedTask;
            }

            var things = _thingRepository
                .Where(x => x.Id.Gateway.Equals(argument, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();
            
            ListThings(things);
            
            return Task.CompletedTask;
        }

        private void ListThings(IReadOnlyCollection<IThing> things)
        {
            var maxNameWidth = things.Max(x => x.Name.Length) + 2;
            var maxIdWidth = things.Max(x => x.Id.ToString().Length) + 2;

            Output.WriteLine("List all things of gateway");
            Output.WriteLine();
            
            Gui.PrintColumns(
                new ConsoleTextColumn {Text = "Name", Width = maxNameWidth},
                new ConsoleTextColumn {Text = "Id", Width = maxIdWidth},
                new ConsoleTextColumn {Text = "State"});
            
            Gui.PrintColumns(
                new ConsoleTextColumn {Text = "----", Width = maxNameWidth},
                new ConsoleTextColumn {Text = "--", Width = maxIdWidth},
                new ConsoleTextColumn {Text = "-----"});
            
            things.ForEach(thing => 
                Gui.PrintColumns(
                    new ConsoleTextColumn {Text = thing.Name, Width = maxNameWidth},
                    new ConsoleTextColumn {Text = thing.Id.ToString(), Width = maxIdWidth},
                    new ConsoleTextColumn {Text = thing.State.ToString()}));
        }

        public override string CommandName => "list-things";
    }
}