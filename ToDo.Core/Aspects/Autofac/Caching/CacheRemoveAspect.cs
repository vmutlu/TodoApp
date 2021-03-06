using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Core.CrossCuttingConcerns.Caching;
using ToDo.Core.Utilities.Interceptors;
using ToDo.Core.Utilities.IoC;

namespace ToDo.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
