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
                cfg.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                cfg.CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

            });
            return config;
        }
    }
}
