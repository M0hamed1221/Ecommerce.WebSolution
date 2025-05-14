using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureRegistraton (this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<StoreIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityStoreConnection")));
            services.AddScoped<IDbIntitializer, DbIntializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var redisConn = configuration.GetConnectionString("RdisConnection");
                return ConnectionMultiplexer.Connect(redisConn!);
            });
       
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.RegisterIdentity();
            return services;
        }
        private static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>
                (config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequiredLength = 8;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireDigit = false;


                })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreDbContext>();
            return services;
        }
    }
}
