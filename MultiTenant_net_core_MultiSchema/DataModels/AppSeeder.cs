﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultiTenant_net_core_MultiSchema.DataModels.Contexts;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant_net_core_MultiSchema.DataModels
{
    public class AppSeeder
    {
        private readonly TenantContext _tenantContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApplicationContext> _logger;
        public AppSeeder(TenantContext tenantContext, IConfiguration configuration,ILogger<ApplicationContext> logger)
        {
            _logger = logger;
            _tenantContext = tenantContext;
            _configuration = configuration;
        }
        public void AddMigrations()
        {
            if (_tenantContext.Tenants.Any())
            {
                var tenants = _tenantContext.Tenants.ToList();
                foreach (Tenant newtenant in tenants)
                {
                    try
                    {
                        var dbContextOptionsBuilder_ = new DbContextOptionsBuilder<ApplicationContext>();
                        dbContextOptionsBuilder_.UseNpgsql(newtenant.ConnectionStringName);
                        ApplicationContext context = new ApplicationContext(dbContextOptionsBuilder_.Options);
                        if (context.Database.EnsureCreated())
                        {
                            context.Database.Migrate();
                        }
                        else
                        {
                            context.Database.Migrate();
                        }
                    }
                    catch
                    {
                    }
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<TenantContext>();
                    dbContextOptionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnectionString"));
                }
            }
            else {
                _tenantContext.Database.EnsureCreated();
            }
        }
    }
}
