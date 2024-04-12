using CrudTestApplication.Application.Interfaces;
using CrudTestApplication.Application.Services;
using CrudTestApplication.Core;
using CrudTestApplication.Core.Interfaces;
using CrudTestApplication.Infrastructure.Logging;
using CrudTestApplication.Infrastructure.Data;
using CrudTestApplication.Infrastructure.Repository;
using CrudTestApplication.Web.HealthChecks;
using CrudTestApplication.Web.Interfaces;
using CrudTestApplication.Web.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrudTestApplication.Core.Repositories;
using CrudTestApplication.Core.Repositories.Base;
using CrudTestApplication.Core.Configuration;
using CrudTestApplication.Infrastructure.Repository.Base;

namespace CrudTestApplication.Web
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
            // aspnetrun dependencies
            ConfigureCrudTestApplicationServices(services);            

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
        }        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for customerion scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureCrudTestApplicationServices(IServiceCollection services)
        {
            // Add Core Layer
            services.Configure<CrudTestApplicationSettings>(Configuration);

            // Add Infrastructure Layer
            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            // Add Application Layer
            services.AddScoped<ICustomerService, CustomerService>();


            // Add Web Layer
            services.AddAutoMapper(typeof(Startup)); // Add AutoMapper
            services.AddScoped<IIndexPageService, IndexPageService>();
            services.AddScoped<ICustomerPageService, CustomerPageService>();

            // Add Miscellaneous
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddCheck<IndexPageHealthCheck>("home_page_health_check");
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<CrudTestApplicationContext>(c =>
                c.UseInMemoryDatabase("CrudTestApplicationConnection"));

            //// use real database
            //services.AddDbContext<CrudTestApplicationContext>(c =>
            //    c.UseSqlServer(Configuration.GetConnectionString("CrudTestApplicationConnection")));
        }
    }
}
