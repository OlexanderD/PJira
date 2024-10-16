using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pjira.Application.Common.Interfaces;
using Pjira.Infrastructure.Data;

namespace Pjira.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MssqlConnectionString");

            services.AddDbContext<PjiraDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IPjiraDbContext, PjiraDbContext>();

            return services;
        }
    }
}
