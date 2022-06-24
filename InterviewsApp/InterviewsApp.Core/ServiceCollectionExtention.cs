using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InterviewsApp.Core
{
    public static class ServiceCollectionExtention
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IPositionService, PositionService>();
        }
    }
}
