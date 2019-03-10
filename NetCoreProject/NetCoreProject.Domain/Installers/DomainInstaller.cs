using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreProject.Data.Common.Installers;
using NetCoreProject.Domain.Interfaces;
using NetCoreProject.Domain.Services;

namespace NetCoreProject.Domain.Installers
{
    public class DomainInstaller
    {
        private readonly IConfiguration _configuration;

        public DomainInstaller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Install(IServiceCollection services)
        {
            var dataInstaller = new DataInstaller(_configuration);
            dataInstaller.Install(services);

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
