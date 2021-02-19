using AuthenticationAPI.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvp24Hours.WebAPI.Extensions;

namespace AuthenticationAPI.WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddJwtBearer(Configuration);
            services.AddResponseCompression();
            services.AddDocumentation();
            services.AddPolicies();
            services.AddMyServices();

            #region [ Mvp24Hours ]
            services.AddMvp24HoursService();
            services.AddMvp24HoursJsonService();
            services.AddMvp24HoursZipService();
            #endregion

            services.AddControllers();
            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();

            if (!env.IsProduction())
            {
                app.UseDocumentation();
            }

            app.UseMvp24Hours();
        }
    }
}
