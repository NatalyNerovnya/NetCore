using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Data.Common.Mappers;
using NetCoreProject.Data.Common.Repositories;
using NetCoreProject.Data.EFModels;

namespace NetCoreProject.Data.Common.Installers
{
    public class DataInstaller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionStringName = "NorthwindConnectionString";

        public DataInstaller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Install(IServiceCollection services)
        {
            var connection = _configuration.GetConnectionString(_connectionStringName);
            services.AddDbContext<NorthwindContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
