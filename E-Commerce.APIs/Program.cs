using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Helpers;
using E_Commerce.APIs.Middlewares;
using E_Commerce.Core.Entities;
using E_Commerce.Core.RepostriesContruct;
using E_Commerce.Repository;
using E_Commerce.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
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

            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvides) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
            builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            builder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.AddAutoMapper(option => option.AddProfile(new MappingProfiles()));
            builder.Services.AddTransient<ProductPictureUrlResolver>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState
                                                   .Where(P => P.Value.Errors.Count > 0)
                                                   .SelectMany(P => P.Value.Errors)
                                                   .Select(E => E.ErrorMessage)
                                                   .ToList();
                    var response = new ApiValidationErrorResponse() { Errors = errors };

                    return new BadRequestObjectResult(response);
                };

            });
            builder.Services.AddTransient<ExceptionMiddleware>();

            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<StoreContext>(); // Ask CLR For Create Object From StoreContext Explicitly

            var loggerFactor = service.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreContextSeeding.SeedAsync(context);
                      
            }
            catch (Exception ex)
            {
                  
                var logger = loggerFactor.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while applying database migrations.");
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            

            // Configure the HTTP request pipeline.

            #region Configure Midelwares "Pipelines"
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
