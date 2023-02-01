using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Services;
using InterviewsApp.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InterviewsApp.Core
{
    public static class ServiceCollectionExtention
    {
        public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgresDatabase(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IAuthService, JwtService>();

            services.AddScoped<ILocalizationService, LocalizationService>();
        }
    }
}
