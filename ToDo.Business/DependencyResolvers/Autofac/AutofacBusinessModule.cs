using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using ToDo.Business.Abstract;
using ToDo.Business.Concrete;
using ToDo.Core.Utilities.Interceptors;
using ToDo.Core.Utilities.Security.JWT;
using ToDo.DataAccess.Abstract;
using ToDo.DataAccess.Concrete.EntityFramework;

namespace ToDo.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfTodoDal>().As<ITodoDal>().SingleInstance();
            builder.RegisterType<TodoManager>().As<ITodoService>().SingleInstance();

            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();

            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();


        }
    }
}
