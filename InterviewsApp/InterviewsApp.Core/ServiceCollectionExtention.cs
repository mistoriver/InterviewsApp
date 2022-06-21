using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core
{
    public static class ServiceCollectionExtention
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
