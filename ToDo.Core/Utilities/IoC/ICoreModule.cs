using Microsoft.Extensions.DependencyInjection;

namespace ToDo.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
