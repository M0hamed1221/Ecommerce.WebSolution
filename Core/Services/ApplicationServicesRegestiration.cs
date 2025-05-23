using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using Services.Specfications;
using ServicesAbstracion;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegestiration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            //services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<IProdectService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICachService, Cachservice>();



            services.AddScoped<Func<IProdectService>>(provider => ()
               => provider.GetRequiredService<IProdectService>());

            services.AddScoped<Func<IBasketService>>(provider => ()
               => provider.GetRequiredService<IBasketService>());

            services.AddScoped<Func<IAuthenticationService>>(provider => ()
               => provider.GetRequiredService<IAuthenticationService>());

            services.AddScoped<Func<IOrderService>>(provider => ()
                => provider.GetRequiredService<IOrderService>());
            services.AddScoped<Func<ICachService>>(provider => ()
              => provider.GetRequiredService<ICachService>());

            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            return services;
        }
    }
}
