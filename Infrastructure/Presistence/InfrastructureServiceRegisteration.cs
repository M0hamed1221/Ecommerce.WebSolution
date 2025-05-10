using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
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
            services.AddScoped<IDbIntitializer, DbIntializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var redisConn = configuration.GetConnectionString("RdisConnection");
                return ConnectionMultiplexer.Connect(redisConn!);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
    }
}
