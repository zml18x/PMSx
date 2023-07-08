using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;
using PMS.Infrastructure.Repository;
using PMS.Infrastructure.Services;
using PMS.Infrastructure.Services.Interfaces;

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
        }
    }
}