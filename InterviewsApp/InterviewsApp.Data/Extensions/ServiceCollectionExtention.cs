using System;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewsApp.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрация зависимостей бд
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация проекта</param>
        /// <exception cref="Exception">Возникает при отсутствии строки подключения в файле конфигурации проекта</exception>
        public static void AddPostgresDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(InterviewsContext)) ??
                                   throw new Exception("Пустая строка подключения.");

            services.AddDbContext<InterviewsContext>(builder => builder.UseNpgsql(connectionString));

            services.AddScoped<IRepository<CompanyEntity>, CompanyRepository>();
            services.AddScoped<IRepository<UserEntity>, UserRepository>();
            services.AddScoped<IRepository<InterviewEntity>, GenericRepository<InterviewEntity>>();
            services.AddScoped<IRepository<PositionEntity>, GenericRepository<PositionEntity>>();
            services.AddScoped<IRepository<UserEntity>, UserRepository>();
            services.AddScoped<IRepository<LocalizationEntity>, GenericRepository<LocalizationEntity>>();
        }
    }
}
