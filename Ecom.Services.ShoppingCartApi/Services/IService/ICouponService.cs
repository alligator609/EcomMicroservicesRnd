using Ecom.Services.ShoppingCartApi.Models.Dtos;

namespace Ecom.Services.ShoppingCartApi.Services.IService
{
    public interface ICouponService
    {
        public Task<CouponDto> GetCoupon(string couponCode);
    }
}
