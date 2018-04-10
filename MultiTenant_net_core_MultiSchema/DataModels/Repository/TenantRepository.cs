using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultiTenant_net_core_MultiSchema.DataModels.Contexts;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenant_net_core_MultiSchema.DataModels.Repository
{
    public class TenantRepository : ITenantRepository
    {
        private readonly TenantContext _context;
        private readonly ILogger<TenantRepository> _logger;
        private readonly IConfiguration _configuration;
        
        public TenantRepository(ILogger<TenantRepository> logger, 
            TenantContext context,
            IConfiguration configuration
            )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        
        public IEnumerable<Tenant> getAllTenants()
        {
            try
            {
                return _context.Tenants;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Tenant getTenantById(int id)
        {
            try
            {
                return _context.Tenants.Where(c => c.Id == id).Last();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void saveAll()
        {
            _context.SaveChanges();
        }

        public string onTenantEntry(Tenant connection_name)
        {
            Environment.SetEnvironmentVariable("SCHEMA_VARIABLE_FOR_APPLICATION_CONTEXT", $"{connection_name.SubDomainName}");
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var connectionString = $"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantCoreSchema{connection_name.SubDomainName};Integrated Security=true;";
            dbContextOptionsBuilder.UseNpgsql(@connectionString);
            ApplicationContext context = new ApplicationContext(dbContextOptionsBuilder.Options);
            if (context.Database.EnsureCreated())
            {
                context.Database.Migrate();
                return connectionString;
            }
            else
            {
                return null;
            }
        }

        public bool addEntity(Tenant newTenant)
        {
            if (_context.Tenants.Where(c => c.SubDomainName == newTenant.SubDomainName).Count() == 0)
            {
                string connectionString = null;
                try
                {
                    connectionString = onTenantEntry(newTenant);
                }
                catch (Exception ex)
                {
                    connectionString = null;
                }
                if (connectionString != null)
                {
                    newTenant.ConnectionStringName = connectionString;
                    _context.Add(newTenant);
                    saveAll();
                }
                //try
                //{
                //    if (connectionString != null)
                //    {
                //        var dbContextOptionsBuilder = new DbContextOptionsBuilder<TenantContext>();
                //        dbContextOptionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnectionString"));
                //        TenantContext tenantContext_temp = new TenantContext(dbContextOptionsBuilder.Options);
                //        Tenant recent_tenant = tenantContext_temp.Tenants.Last();
                //        recent_tenant.ConnectionStringName = connectionString;
                //        tenantContext_temp.SaveChanges();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return false;
                //}
                return true;

            }
            else {
            return false;
            }


        }
    }
}
