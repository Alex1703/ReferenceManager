using Microsoft.Extensions.DependencyInjection;
using ReferenceManager.App.Core.SubscribeTableDependencies;

namespace ReferenceManager.App.Core.MiddlewareExtensions
{
    public static class AplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder)
                   where T : ISqlDependencyService
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<T>();
            service.SubscribeTableDependency();
        }

    }
}
