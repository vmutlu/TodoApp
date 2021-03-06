using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Core.CrossCuttingConcerns.Caching;
using ToDo.Core.CrossCuttingConcerns.Caching.Microsoft;
using ToDo.Core.Utilities.IoC;

namespace ToDo.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
