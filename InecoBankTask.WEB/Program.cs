using InecoBankTask.Entities.Context;
using InecoBankTask.Services;
using Microsoft.EntityFrameworkCore;

namespace InecoBankTask.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.Services.AddDbContext<InecoBankTaskContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("InecoBankTaskConnectionString")));

            builder.Services.AddServicesLayer(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
