namespace CreativeCoders.SmartHal.Config.Base.WebApi
{
    public interface IWebApiConfiguration
    {
        string[] Urls { get; }

        int DefaultPort { get; }

        bool ListenOnLocalhost { get; }

        bool ListenOnHostName { get; }
    }
}