﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using PMS.Core.Entities;

namespace PMS.Infrastructure.Data.Context
{
    public class PmsDbContext : DbContext
    {
        public PmsDbContext() { }

        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options) { }



        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<PropertyAdditionalService> PropertyAdditionalServices { get; set; }
        public DbSet<RoomAdditionalService> RoomAdditionalServices { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}