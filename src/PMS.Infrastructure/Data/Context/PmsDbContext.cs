using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PMS.Infrastructure.Data.Context
{
    public class PmsDbContext : DbContext
    {
        public PmsDbContext() { }

        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options) { }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}