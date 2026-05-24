using AutoMapper;
using IxClouds.API.DTOs.Request;
using IxClouds.API.DTOs.Response;
using IxClouds.Domain.Entities;
namespace IxClouds.API.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponseDto>().ForMember(dest => dest.StockStatus, opt => opt.MapFrom(src => src.Stock <= 2 ? "Critical" : src.Stock <= 10 ? "Low" : "Normal"));
        CreateMap<CreateProductRequestDto, Product>();
        CreateMap<UpdateProductRequestDto, Product>();
        CreateMap<Sale, SaleResponseDto>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.SaleDetails));
        CreateMap<SaleDetail, SaleDetailResponseDto>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => $"{src.Product.Brand} {src.Product.PhoneModel}"));
    }
}
