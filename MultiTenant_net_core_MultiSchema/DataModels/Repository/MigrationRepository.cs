using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenant_net_core_MultiSchema.DataModels.Contexts;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using MultiTenant_net_core_MultiSchema.ViewModels;

namespace MultiTenant_net_core_MultiSchema.DataModels.Repository
{
    public class MigrationRepository : IMigrationRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly TenantContext _tenantContext;
        private readonly IConfiguration _configuration;
        public MigrationRepository(ApplicationContext applicationContext,TenantContext tenantContext,IConfiguration configuration)
        {
            _applicationContext = applicationContext;
            _tenantContext = tenantContext;
            _configuration = configuration;
        }

        public void AddMigration()
        {
            var tenants = _tenantContext.Tenants.ToList();
            foreach (Tenant newtenant in tenants)
            {
                try
                {
                    var dbContextOptionsBuilder_ = new DbContextOptionsBuilder<ApplicationContext>();
                    dbContextOptionsBuilder_.UseNpgsql(newtenant.ConnectionStringName);
                    if (_applicationContext.Database.EnsureCreated())
                    {
                        _applicationContext.Database.Migrate();
                    }
                }
                catch
                {
                }
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<TenantContext>();
                dbContextOptionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnectionString"));
            }
            
        }
    }
}
