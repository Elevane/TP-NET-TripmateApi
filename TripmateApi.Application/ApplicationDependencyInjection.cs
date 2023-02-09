using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TripmateApi.Application.Common.Options;
using TripmateApi.Application.Services.Authentification;
using TripmateApi.Application.Services.Authentification.Interfaces;

namespace TripmateApi.Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserService>();
        }
    }
}
