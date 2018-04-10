using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MultiTenant_net_core_MultiSchema.DataModels.Contexts;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant_net_core_MultiSchema.DataModels.MigrationConfiguration
{
    public class MigrationDesignForMainContext : IDesignTimeDbContextFactory<TenantContext>
    {
        public TenantContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TenantContext> tenantOptions = new DbContextOptionsBuilder<TenantContext>();
            TenantContext tenantContext = new TenantContext(tenantOptions.Options);
            foreach (Tenant tenant in tenantContext.Tenants.ToList())
            {
                DbContextOptionsBuilder<ApplicationContext> appOptions = new DbContextOptionsBuilder<ApplicationContext>();
                appOptions.UseNpgsql(tenant.ConnectionStringName);
                ApplicationContext applicationContext = new ApplicationContext(appOptions.Options);
                applicationContext.Database.EnsureCreated();
                applicationContext.Database.Migrate();
            }
            return tenantContext;
        }
    }
}
