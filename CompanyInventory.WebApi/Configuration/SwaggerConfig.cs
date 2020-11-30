using CompanyInventory.Common.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CompanyInventory.WebApi.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services)
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

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Schemes.basic
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseSwaggerSchema(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company Inventory API V1");
            });

        } 
    }
}