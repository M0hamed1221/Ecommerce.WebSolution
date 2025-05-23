
using Domain.Contracts;
using Ecommerce.Web.Factroies;
using Ecommerce.Web.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstracion;
using Shared.ErrorModles;
using System.Threading.Tasks;

namespace Ecommerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddInfrastructureRegistraton(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddWebApplicationService(builder.Configuration);
             var app = builder.Build();
            await app.IntializeDbAsync();

            app.UseMiddleware<CustomExceptionHandlerMiddelware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
       
    }
}
