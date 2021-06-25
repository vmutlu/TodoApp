using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;
using ToDo.Core.Aspects.Autofac.Exception;
using ToDo.Core.Aspects.Autofac.Performance;
using ToDo.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace ToDo.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
