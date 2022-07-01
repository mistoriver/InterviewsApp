using InterviewsApp.Core;
using InterviewsApp.Data.Extensions;
using InterviewsApp.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterviewsApp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
            // Add services to the container.
            builder.Services.AddCoreServices(builder.Configuration);

            builder.Services.AddCustomAuthentication(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.ApplyMigrations();

            app.Run();
        }
    }
}