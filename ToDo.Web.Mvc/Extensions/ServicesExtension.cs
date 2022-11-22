using ToDo.Domain.Interface;
using ToDo.Infra.Data.Repositories;

namespace ToDo.Web.Mvc.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            return services;
        }
    }
}
