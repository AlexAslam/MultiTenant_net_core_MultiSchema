using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using MultiTenant_net_core_MultiSchema.DataModels.Repository;
namespace MultiTenant_net_core_MultiSchema.DataModels.Contexts
{
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> options) : base(options)
        {
            System.Console.WriteLine("Im in Contructor");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            System.Console.WriteLine("Im in OnModelCreating");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            System.Console.WriteLine("Im in Configuring");
            optionsBuilder.UseNpgsql(@"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantCoreSchema;Integrated Security=true;");
        }
        public DbSet<Tenant> Tenants { get; set; }
        
    }
}
