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
using CompanyInventory.Common;
using CompanyInventory.Common.Consts;
using CompanyInventory.Repository;
using CompanyInventory.WebApi.Handlers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Converters;

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
            services.AddMvc().AddFluentValidation();
            
            services.AddControllersWithViews()
                .AddNewtonsoftJson(opts => { opts.SerializerSettings.Converters.Add(new StringEnumConverter()); });

            AddSwagger(services);

            services.AddDbContext<CompanyInventoryContext>(opt
                => opt.UseSqlServer(_configuration.GetConnectionString("Default")));

            services.AddAuthentication(Schemes.basicAuth)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(Schemes.basicAuth, null);

            services.AddAutofac();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitTasks(app);

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company Inventory API V1"); });

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
        
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Company Inventory API", Version = "v1"});

                c.AddSecurityDefinition(Schemes.basic, new OpenApiSecurityScheme
                {
                    Name = Headers.authorizationHeaderName,
                    Type = SecuritySchemeType.Http,
                    Scheme = Schemes.basic,
                    In = ParameterLocation.Header
                });
            });
            
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}