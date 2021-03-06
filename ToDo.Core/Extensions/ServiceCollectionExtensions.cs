using Microsoft.Extensions.DependencyInjection;
using ToDo.Core.Utilities.IoC;

namespace ToDo.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
