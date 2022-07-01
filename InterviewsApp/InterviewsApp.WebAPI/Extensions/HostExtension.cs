using InterviewsApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterviewsApp.WebAPI.Extensions
{
    public static class HostExtension
    {
        /// <summary>
        /// Применить миграции EF Core
        /// </summary>
        /// <param name="host">Хост</param>
        /// <returns>Хост</returns>
        public static IHost ApplyMigrations(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<InterviewsContext>();
            context.Database.Migrate();
            return host;
        }
    }
}
