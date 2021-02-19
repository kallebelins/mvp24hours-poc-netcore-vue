using AuthenticationAPI.Application.Identity;
using AuthenticationAPI.Application.Logic;
using AuthenticationAPI.Core.Contract.Identity;
using AuthenticationAPI.Core.Contract.Logic;
using AuthenticationAPI.Core.DTOs.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AuthenticationAPI.WebAPI.Extensions
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
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddHostedService<JwtRefreshTokenCache>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtTokenConfig = configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication API", Version = "v1" });

                //c.DocumentFilter<CustomSwaggerFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
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
