using Microsoft.EntityFrameworkCore;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using System;

namespace MultiTenant_net_core_MultiSchema.DataModels.Contexts
{
    public class ApplicationContext : DbContext
    { 
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (Environment.GetEnvironmentVariable("SCHEMA_VARIABLE_FOR_APPLICATION_CONTEXT") != null && Environment.GetEnvironmentVariable("SCHEMA_VARIABLE_FOR_APPLICATION_CONTEXT") != "")
            {
                modelBuilder.HasDefaultSchema(Environment.GetEnvironmentVariable("SCHEMA_VARIABLE_FOR_APPLICATION_CONTEXT"));
            }
            else {
                modelBuilder.HasDefaultSchema("public");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT") !=null && Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT") != "")
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT"));
            }
        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
