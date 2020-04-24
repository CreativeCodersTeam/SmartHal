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
    public class GetThingInfoCommand : ConsoleCommandBase
    {
        private readonly IThingRepository _thingRepository;

        public GetThingInfoCommand(IThingRepository thingRepository)
        {
            _thingRepository = thingRepository;
        }
        
        public override Task ExecuteAsync(IReadOnlyCollection<string> arguments)
        {
            var thingName = arguments.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(thingName))
            {
                Output.WriteLine("No thing name specified");
                
                return Task.CompletedTask;
            }

            var thing = _thingRepository.FirstOrDefault(x => x.Name == thingName);

            if (thing == null)
            {
                Output.WriteLine($"Thing '{thingName}' not found");

                return Task.CompletedTask;
            }

            Output.WriteLine($"Name:  {thing.Name}");
            Output.WriteLine($"Id:    {thing.Id}");
            Output.WriteLine($"State: {thing.State}");
            Output.WriteLine();
            
            PrintChannels(thing.Channels);
            
            return Task.CompletedTask;
        }

        private void PrintChannels(IReadOnlyCollection<IThingChannel> channels)
        {
            Output.WriteLine("Channels:");

            var maxNameWidth = channels.Max(x => x.Name.Length) + 2;
            
            Gui.PrintColumns(
                new ConsoleTextColumn{Text = "Name", Width = maxNameWidth},
                new ConsoleTextColumn{Text = "Id"});
            Gui.PrintColumns(
                new ConsoleTextColumn{Text = "----", Width = maxNameWidth},
                new ConsoleTextColumn{Text = "--"});

            channels.ForEach(channel =>
                Gui.PrintColumns(
                    new ConsoleTextColumn{Text = channel.Name, Width = maxNameWidth},
                    new ConsoleTextColumn{Text = channel.Id.ToString()}));
        }

        public override string CommandName => "get-thing-info";
    }
}