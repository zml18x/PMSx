using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// This method is part of the database migration mechanism that automatically
        /// the database schema to the latest version. If the database schema does not
        /// exist, it will be created. If the shcema already exist,
        /// all available migrations will be applied to bring the schema
        /// to the latest version.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopeProvider = scope.ServiceProvider;
                var context = scopeProvider.GetRequiredService<PmsDbContext>();
                context.Database.Migrate();
            }
        }
    }
}