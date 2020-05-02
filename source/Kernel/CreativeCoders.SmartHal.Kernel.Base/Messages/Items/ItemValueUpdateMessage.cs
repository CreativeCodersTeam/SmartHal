namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Items
{
    public class ItemValueUpdateMessage : SmartHalMessageBase
    {
        public ItemValueUpdateMessage(string itemName, object newValue)
        {
            ItemName = itemName;
            NewValue = newValue;
        }
        
        public string ItemName { get; }
        
        public object NewValue { get; }
    }
}