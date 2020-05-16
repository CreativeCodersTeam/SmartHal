using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class ListItemsCommand : ConsoleCommandBase
    {
        private readonly IItemRepository _itemRepository;

        public ListItemsCommand(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        
        public override Task ExecuteAsync(string[] arguments)
        {
            var nameWidth = _itemRepository.Max(item => item.Name.Length) + 2;

            Gui.PrintColumns(
                new ConsoleTextColumn {Text = "Name", Width = nameWidth},
                new ConsoleTextColumn {Text = "Value"});
            
            Gui.PrintColumns(
                new ConsoleTextColumn {Text = "----", Width = nameWidth},
                new ConsoleTextColumn {Text = "-----"});
            
            _itemRepository.ForEach(item => PrintItem(item, nameWidth));
            
            return Task.CompletedTask;
        }

        private void PrintItem(IItem item, int nameWidth)
        {
            Gui.PrintColumns(
                new ConsoleTextColumn{Text = item.Name, Width = nameWidth},
                new ConsoleTextColumn{Text = item.Value.ToStringSafe()});
        }

        public override string CommandName => "list-items";
    }
}