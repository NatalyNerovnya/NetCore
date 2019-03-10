using AutoMapper;
using NetCoreProject.Common;
using NetCoreProject.Data.EFModels;
using Product = NetCoreProject.Common.Product;

namespace NetCoreProject.Data.Common.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Categories, Category>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<Suppliers, Supplier>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName));

            CreateMap<Products, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.CompanyName));

            CreateMap<Product, Products>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
