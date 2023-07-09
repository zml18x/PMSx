using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;
using PMS.Infrastructure.Exceptions;
using PMS.Infrastructure.Repository;
using PMS.Infrastructure.Services;
using PMS.Infrastructure.Services.Interfaces;
using System.Text;

namespace PMS.Infrastructure.Container
{
    public static class InfrastructureDependencies
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //EntityFramework
            services.AddDbContext<PmsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            //Repository
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserProfileRepository,UserProfileRepository>();

            //Services
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IJwtService,JwtService>();


            //JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration?["JWT:Key"] ?? throw new MissingConfigurationException("JWT key configuration is missing or empty")))
                };
            });
        }
    }
}