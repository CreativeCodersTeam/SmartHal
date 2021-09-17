using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

#nullable enable
namespace CreativeCoders.SmartHal.Web.CommandCenter.Client.Localization
{
    public static class LocalizationServiceCollectionExtensions
    {
        public static void SetupLocalization(this IServiceCollection services, string resourcesPath)
        {
            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = resourcesPath;
            });

            services.AddSingleton<IStringLocalizer, DefaultStringLocalizer>();

            services.AddSingleton(typeof(IExtendedStringLocalizer<>), typeof(DefaultExtendedStringLocalizer<>));
        }
    }
}