using AutoMapper;
using Ecom.Services.ShoppingCartApi.Models;
using Ecom.Services.ShoppingCartApi.Models.Dtos;

namespace Ecom.Services.ShoppingCartApi
{
    public class MappingConfig
    {
        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                cfg.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();

            });
            return config;
        }
    }
}
