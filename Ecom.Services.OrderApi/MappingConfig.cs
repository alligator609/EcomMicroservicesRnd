using AutoMapper;
using Ecom.Services.OrderApi.Models;
using Ecom.Services.OrderApi.Models.Dtos;

namespace Ecom.Services.OrderApi
{
    public class MappingConfig
    {
        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, opt => opt.MapFrom(src => src.OrderTotal)).ReverseMap();
                cfg.CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ReverseMap();

                cfg.CreateMap<OrderDetails, CartDetailsDto>().ReverseMap();
                cfg.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                cfg.CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();


            });
            return config;
        }
    }
}
