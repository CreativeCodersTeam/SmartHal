namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Items
{
    public class ItemValueChangedMessage : SmartHalMessageBase
    {
        public ItemValueChangedMessage(string itemName, object newValue)
        {
            ItemName = itemName;
            NewValue = newValue;
        }
        
        public string ItemName { get; }
        
        public object NewValue { get; }
    }
}