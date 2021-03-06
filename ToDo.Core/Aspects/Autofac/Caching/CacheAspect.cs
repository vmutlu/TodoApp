using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ToDo.Core.CrossCuttingConcerns.Caching;
using ToDo.Core.Utilities.Interceptors;
using ToDo.Core.Utilities.IoC;

namespace ToDo.Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // namespace al
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            if (_cacheManager.IsAdd(key))//cache de varmı
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
