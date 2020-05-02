namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Items
{
    public class SendCommandToItemMessage : SmartHalMessageBase
    {
        public SendCommandToItemMessage(string itemName, object commandValue)
        {
            ItemName = itemName;
            CommandValue = commandValue;
        }
        
        public string ItemName { get; }
        
        public object CommandValue { get; }
    }
}