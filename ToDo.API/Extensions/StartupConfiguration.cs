using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ToDo.Core.DependencyResolvers;
using ToDo.Core.Extensions;
using ToDo.Core.Services.Abstract;
using ToDo.Core.Services.Concrete;
using ToDo.Core.Utilities.IoC;
using ToDo.Core.Utilities.Security.Encryption;
using ToDo.Core.Utilities.Security.JWT;
using ToDo.DataAccess.Concrete;

namespace ToDo.API.Extensions
{
    public static class StartupConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, string connectionStrings)
        {
            services.AddDbContext<TodoContext>(opts => opts.UseSqlServer(connectionStrings, option => { option.MigrationsAssembly("ToDo.DataAccess"); }));
            services.AddDbContext<TodoContext>(q => q.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        public static void ConfigureDependecies(this IServiceCollection services, TokenOptions tokenOption)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IPaginationUriService>(opt =>
            {
                var httpContextAccessor = opt.GetRequiredService<IHttpContextAccessor>();
                return new PaginationUriService(httpContextAccessor);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers().AddNewtonsoftJson();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOption.Issuer,
                        ValidAudience = tokenOption.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOption.SecurityKey)
                    };
                });
            services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });
        }
    }
}
