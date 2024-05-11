using AutoMapper;
using Ecom.Services.CouponApi.Models;
using Ecom.Services.CouponApi.Models.Dtos;

namespace Ecom.Services.CouponApi
{
    public class MappingConfig
    {
        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            return config;
        }
    }
}
