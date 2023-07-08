using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;
using PMS.Infrastructure.Repository;

namespace PMS.Infrastructure.Container
{
    public static class InfrastructureDependencies
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PmsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));



            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserProfileRepository,UserProfileRepository>();
        }
    }
}