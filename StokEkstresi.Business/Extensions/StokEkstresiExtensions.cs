using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StokEkstresi.Business.Abstacts;
using StokEkstresi.Business.Concretes;
using StokEkstresi.DataAccess.Concretes.Contexts;

namespace StokEkstresi.Business.Extensions
{
    public static class StokEkstresiExtensions
    {
        public static IServiceCollection AddStokEkstresiService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IStokEkstresiService, StokEkstresiService>();

            return services;
        }



    }
}
