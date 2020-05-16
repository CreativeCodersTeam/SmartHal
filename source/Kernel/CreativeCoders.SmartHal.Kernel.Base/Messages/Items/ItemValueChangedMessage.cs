namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Items
{
    public class ItemValueChangedMessage : SmartHalMessageBase
    {
        public ItemValueChangedMessage(string itemName, object newValue, object oldValue)
        {
            ItemName = itemName;
            NewValue = newValue;
            OldValue = oldValue;
        }
        
        public string ItemName { get; }
        
        public object NewValue { get; }
        
        public object OldValue { get; }
    }
}