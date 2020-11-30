using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyInventory.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;
using CompanyInventory.Common;
using CompanyInventory.Common.Consts;
using CompanyInventory.Repository;
using CompanyInventory.WebApi.Handlers;
using FluentValidation.AspNetCore;
using LaYumba.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Converters;
using System;
using CompanyInventory.Models.Company;
using CompanyInventory.WebApi.Configuration;
using Newtonsoft.Json;

namespace CompanyInventory.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CompanySearchValidator>());

            services.AddControllersWithViews()
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddSwagger();

            services.AddDbContext<CompanyInventoryContext>(opt
                => opt.UseSqlServer(_configuration.GetConnectionString("Default")));

            services.AddAuthentication(Schemes.basicAuth)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(Schemes.basicAuth, null);

            services.AddAutofac();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context => { await context.SetExceptionResponseAsync(); });
            });

            InitTasks(app);

            app.UseSwaggerSchema();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterRepositories();
        }

        private void InitTasks(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CompanyInventoryContext>();
                context.Database.Migrate();
            }
        }
    }
}