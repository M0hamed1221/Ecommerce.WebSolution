using Domain.Contracts;
using Ecommerce.Web.Factroies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Authentication;
using System.Text;

namespace Ecommerce.Web
{
    public static class Extensions
    {
        public static IServiceCollection AddWebApplicationService (this IServiceCollection services, IConfiguration configuration )
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           services.AddSwagerServices();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Func<ActionContexy,IactionResult>
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiVaildtionResponse;


            });
            services.ConfigureJWT (configuration);
            return services;
        }

        private static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JWTOptions").Get<JWTOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });
          
        }
        private static void AddSwagerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public static async Task<WebApplication> IntializeDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var databaseintializer = scope.ServiceProvider.GetRequiredService<IDbIntitializer>();
            await databaseintializer.IntializeAsync();
            await databaseintializer.IntializeIdentityAsync();

            return app;
        }
    }
}
 