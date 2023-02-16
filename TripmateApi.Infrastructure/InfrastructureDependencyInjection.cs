using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripmateApi.Infrastructure.Contexts;
using TripmateApi.Infrastructure.Contexts.Interfaces;
using TripmateApi.Infrastructure.Repositories;
using TripmateApi.Infrastructure.Repositories.Trajets;

namespace TripmateApi.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastrucure(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddScoped<UserRepository>();
            services.AddScoped<TrajetsRepository>();   
           services.AddDbContext<ITripmateContext, TripMateSqlContext>(options =>
            {
                string connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    mySqlOptions =>
                        mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                    ));
            });
        }
    }
}
