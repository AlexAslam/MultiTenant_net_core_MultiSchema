﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant_net_core_MultiSchema.DataModels;
using MultiTenant_net_core_MultiSchema.DataModels.Contexts;
using MultiTenant_net_core_MultiSchema.DataModels.Repository;

namespace MultiTenantCore
{
    public class Startup
    {
        private readonly IConfiguration _config;
        
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TenantContext>();
            services.AddDbContext<ApplicationContext>();
            
            services.AddAutoMapper();
            //services.AddTransient<AppSeeder>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IMigrationRepository, MigrationRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware();
            app.UseMvc();
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var seeder = scope.ServiceProvider.GetService<AppSeeder>();
            //    seeder.AddMigrations();
            //}
        }
    }
}
