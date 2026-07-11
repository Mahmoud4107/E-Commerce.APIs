using E_Commerce.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace E_Commerce.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<StoreContext>(); // Ask CLR For Create Object From StoreContext Explicitly

            var loggerFactor = service.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                      
            }
            catch (Exception ex)
            {
                  
                var logger = loggerFactor.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while applying database migrations.");
            }

            

            // Configure the HTTP request pipeline.

            #region Configure Midelwares "Pipelines"
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
