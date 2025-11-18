using Microsoft.Extensions.DependencyInjection;
using WpfApp.Domain.Interfaces.Services;
using WpfApp.Service.Services;

namespace WpfApp.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBookService, BookService>();
        }
    }
}