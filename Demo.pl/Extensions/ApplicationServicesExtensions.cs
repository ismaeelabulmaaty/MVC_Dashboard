using Demo.BLL.Interfaces;
using Demo.BLL.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.pl.Extensions
{
    public static class ApplicationServicesExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services )
        {
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();

            services.AddScoped<IEmployeeRepo, EmployyeRepo>();
            return services;
        }
    }
}
