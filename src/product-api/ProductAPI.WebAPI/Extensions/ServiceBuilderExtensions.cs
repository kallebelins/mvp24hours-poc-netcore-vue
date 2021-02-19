using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Mvp24Hours.Core.Contract.Infrastructure.Pipe;
using Mvp24Hours.Infrastructure.Pipe;
using ProductAPI.Application.Logic;
using ProductAPI.Core.Contract.Logic;
using ProductAPI.Core.Contract.Pipeline.Builders.Products;
using ProductAPI.MakeUpAPI.Application.Builders.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProductAPI.WebAPI.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            // product
            services.AddScoped<IProductService, ProductService>();

            //  builder product - pipeline
            services.AddScoped<IGetByIdProductBuilder, GetByIdProductBuilder>();
            services.AddScoped<IListProductBuilder, ListProductBuilder>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddPipelines(this IServiceCollection services)
        {
            services.AddTransient<IPipelineAsync, PipelineAsync>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });

                //c.DocumentFilter<CustomSwaggerFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
