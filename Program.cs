
using Inventory_Management.Services;
using InventoryManagement.Models;
using InventoryManagement.Services;

namespace InventoryManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           
            builder.Services.AddControllers();

            builder.Services.AddDbContext<InventoryManagementContext>();
            builder.Services.AddScoped<InventoryService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<CategoryService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy",
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("ReactPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
