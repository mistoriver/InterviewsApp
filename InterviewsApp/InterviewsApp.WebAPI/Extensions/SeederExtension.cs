using InterviewsApp.Data;
using InterviewsApp.Data.Seeding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterviewsApp.WebAPI.Extensions
{
    public static class SeederExtension
    {
        public static void SeedDb(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var seeder = services.GetRequiredService<IDatabaseSeeder>();
            seeder.SeedDatabase();
        }
    }
}
