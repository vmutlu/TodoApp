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
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfTodoDal>().As<ITodoDal>();
            builder.RegisterType<TodoManager>().As<ITodoService>();

            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();

            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();

            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();

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
