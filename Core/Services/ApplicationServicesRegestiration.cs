using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegestiration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            return services;
        }
    }
}
