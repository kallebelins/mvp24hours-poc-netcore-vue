using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvp24Hours.WebAPI.Extensions;
using ProductAPI.WebAPI.Extensions;
using System.Reflection;

namespace ProductAPI.WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCompression();

            services.AddMyServices();
            services.AddDocumentation();
            services.AddPipelines();

            #region [ Mvp24Hours ]
            services.AddMvp24HoursService();
            services.AddMvp24HoursJsonService();
            services.AddMvp24HoursMapService(Assembly.GetExecutingAssembly());
            services.AddMvp24HoursZipService();
            #endregion

            services.AddControllers();
            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandling();

            app.UseStaticFiles();
            app.UseRouting();

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
