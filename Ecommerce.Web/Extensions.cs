using Domain.Contracts;
using Ecommerce.Web.Factroies;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web
{
    public static class Extensions
    {
        public static IServiceCollection AddWebApplicationService (this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           services.AddSwagerServices();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Func<ActionContexy,IactionResult>
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiVaildtionResponse;


            });
            return services;
        }

        private static void AddSwagerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public static async Task IntializeDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var databaseintializer = scope.ServiceProvider.GetRequiredService<IDbIntitializer>();
            await databaseintializer.IntializeAsync();
        }
    }
}
