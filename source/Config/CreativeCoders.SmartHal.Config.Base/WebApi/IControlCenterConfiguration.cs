namespace CreativeCoders.SmartHal.Config.Base.WebApi
{
    public interface IControlCenterConfiguration
    {
        string[] Urls { get; }

        int DefaultPort { get; }

        bool ListenOnLocalhost { get; }

        bool ListenOnHostName { get; }
    }
}