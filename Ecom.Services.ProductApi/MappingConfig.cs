using AutoMapper;
using Ecom.Services.ProductApi.Models;
using Ecom.Services.ProductApi.Models.Dtos;

namespace Ecom.Services.ProductApi
{
    public class MappingConfig
    {
        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
            });
            return config;
        }
    }
}
