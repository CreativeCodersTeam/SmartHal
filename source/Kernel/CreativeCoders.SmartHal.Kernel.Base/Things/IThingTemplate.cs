namespace CreativeCoders.SmartHal.Kernel.Base.Things
{
    public interface IThingTemplate
    {
        bool IsChannelDefined(string channelName);
        
        string Name { get; }
    }
}