using System;
using Microsoft.Extensions.DependencyInjection;

namespace Portal.Apis
{
    public static class ServiceProvider
    {
        public static IServiceProvider Services { get; set; }

        public static TService GetService<TService>()
            where TService : class
        {
            IServiceScope scope = Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            
            return services.GetRequiredService<TService>();
        }
    }
}