using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InterviewsApp.Core.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace InterviewsApp.WebAPI.Extensions
{
    public static class ServiceCollectionExtention
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfigSection = configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authConfigSection);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    var authSettings = authConfigSection.Get<AuthSettings>();
                    byte[] key = Encoding.ASCII.GetBytes(authSettings.Secret);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuerSigningKey = true
                    };
                });
        }
    }
}
