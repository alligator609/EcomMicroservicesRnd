using Ecom.Services.CouponApi.Data;
using Ecom.Services.CouponApi.Models;
using Ecom.Services.CouponApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ResponseDto _responseDto;
        public CouponController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto GetCoupon()
        {
            try
            {
                IEnumerable<Coupon> coupons = _applicationDbContext.Coupons.ToList();
                _responseDto.Result = coupons;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpGet("{id}")]
        public ResponseDto GetCoupon(int id)
        {
            try
            {
                var coupon = _applicationDbContext.Coupons.FirstOrDefault(x => x.Id == id);
                _responseDto.Result = coupon;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPost]
        public ResponseDto CreateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                _applicationDbContext.Coupons.Add(coupon);
                _applicationDbContext.SaveChanges();
                _responseDto.Result = coupon;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPut]
        public ResponseDto UpdateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                _applicationDbContext.Entry(coupon).State = EntityState.Modified;
                _applicationDbContext.SaveChanges();
                _responseDto.Result = coupon;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpDelete("{id}")]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {
                var coupon = _applicationDbContext.Coupons.FirstOrDefault(x => x.Id == id);
                _applicationDbContext.Coupons.Remove(coupon);
                _applicationDbContext.SaveChanges();
                _responseDto.Result = coupon;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
