using Common.EntityFramework;
using Common.Repositories;
using Common.Services;
using Common.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NetCoreMentoring
{
    public class Startup
    {
        //TODO: access modifier, readonly
        ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            InjectDependences(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(errorApp =>
                {
                    //TODO: remove, setup only exception handling path
                    //TODO: move logging logic into error page, RequestId and last error can be retrieved from HttpContext
                    //RequestId = HttpContext.TraceIdentifier;
                    //var error = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
                    errorApp.Run(async context =>
                    {
                        await BuildCustomError(context);
                    });
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async Task BuildCustomError(HttpContext context)
        {
            //TODO: remove this from startup, use custom error page
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html";

            await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            await context.Response.WriteAsync("ERROR!<br>Please find more information in the log file<br>\r\n");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError(exceptionHandlerPathFeature.Error, "Error is handled by custom handler.");

            await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
            await context.Response.WriteAsync("<br><br><a href=\"/\">Home</a><br>\r\n");
            await context.Response.WriteAsync("</body></html>\r\n");
            await context.Response.WriteAsync(new string(' ', 512)); // IE padding
        }

        private void InjectDependences(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("NorthwindConnectionString");
            services.AddDbContext<NorthwindContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));

            services.AddTransient<ISettings, ConfigurationSettings>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
