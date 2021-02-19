using CustomerAPI.Infrastructure.Data;
using CustomerAPI.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvp24Hours.WebAPI.Extensions;
using System.Reflection;

namespace CustomerAPI.WebAPI
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

            services.AddMyDbContext(Configuration);
            services.AddMyServices();
            services.AddDocumentation();

            #region [ Mvp24Hours ]
            services.AddMvp24HoursAllAsync<CustomerDBContext>(Assembly.GetExecutingAssembly());
            #endregion

            services.AddControllers();
            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomerDBContext db)
        {
            // check environment
            app.UseExceptionHandling();

            db.Database.EnsureCreated();

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
