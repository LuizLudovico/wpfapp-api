using WpfApp.Data.Context;
using WpfApp.Data.Repository;
using WpfApp.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>)); 

            var connectionString = "Server=localhost;Port=3306;Database=WEB_API;Uid=root;Pwd=root";

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30)))
            );
        }
    }
}