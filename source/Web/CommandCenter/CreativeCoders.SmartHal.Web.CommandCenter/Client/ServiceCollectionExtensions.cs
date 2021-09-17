using System;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi<T>(this IServiceCollection services, Uri apiUri)
            where T : class
        {
            services.AddRefitClient<T>()
                .ConfigureHttpClient((_, client) => client.BaseAddress = apiUri);

            return services;
        }
    }
}