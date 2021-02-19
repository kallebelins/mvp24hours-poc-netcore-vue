using GatewayAPI.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewayAPI.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPolicies();
            services.AddOcelot(Configuration);
            services.AddSwaggerForOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/gateway");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Gateway API");
                });
            });

            app.UseOcelot().Wait();
        }
    }
}
