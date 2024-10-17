using Microsoft.Extensions.DependencyInjection;
using OoberEats.Application.Services;
namespace OoberEats.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
