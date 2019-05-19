using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.Filters;
using NetCoreMentoring.Infrastructure;
using NetCoreMentoring.Middleware;
using NetCoreProject.Common;
using NetCoreProject.Domain.Installers;

namespace NetCoreMentoring
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.Filters.Add(new LoggingFilterFactory());

                })
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            InjectDependences(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime, ILogger<Startup> logger)
        {
            applicationLifetime.ApplicationStarted.Register(() => { OnApplicationStart(env, logger); });

            app.UseMiddleware<CacheImageMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/DevError");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "images",
                    template: "images/{id}",
                    defaults: new { controller = "Category", action = "GetImage" });
            });
        }

        private void InjectDependences(IServiceCollection services)
        {
            var domainInstaller = new DomainInstaller(Configuration);
            domainInstaller.Install(services);

            services.AddTransient<IValidator<Product>, ProductValidator>();
            services.AddSingleton<IImageCache, InMemoryImageCache>();
        }

        private void OnApplicationStart(IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            logger.LogInformation($"Application is running : {hostingEnvironment.ContentRootPath}");
        }
    }
}
