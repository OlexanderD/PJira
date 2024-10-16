namespace Pjira.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            return services;
        }
    }
}
