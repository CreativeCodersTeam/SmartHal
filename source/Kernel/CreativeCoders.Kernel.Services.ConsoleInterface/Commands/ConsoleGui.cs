using System.Linq;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    public class ConsoleGui
    {
        private readonly IConsoleOutput _output;

        public ConsoleGui(IConsoleOutput output)
        {
            _output = output;
        }

        public void PrintColumns(params ConsoleTextColumn[] columns)
        {
            
            var lineText = string.Join("", columns.Select(GetColumnText));
            
            _output.WriteLine(lineText);
        }

        private static string GetColumnText(ConsoleTextColumn column)
        {
            if (column.Width == -1)
            {
                return column.Text;
            }
            
            var format = $"{{0,{column.Width * -1}}}";
            
            return string.Format(format, column.Text);
        }
    }
}