
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using System.Threading.Tasks;

namespace Ecommerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDbIntitializer, DbIntializer>();
            var app = builder.Build();
            await IntializeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            // app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
        public static async Task IntializeDbAsync(WebApplication app)
        {
            using var scope        = app.Services.CreateScope();
            var databaseintializer = scope.ServiceProvider.GetRequiredService<IDbIntitializer>();
            await databaseintializer.IntializeAsync();
        }
    }
}
