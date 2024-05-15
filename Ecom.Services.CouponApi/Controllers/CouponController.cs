using AutoMapper;
using Ecom.Services.CouponApi.Data;
using Ecom.Services.CouponApi.Models;
using Ecom.Services.CouponApi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ResponseDto _responseDto;
        private  IMapper _mapper;
        public CouponController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto GetCoupon()
        {
            try
            {
                IEnumerable<Coupon> coupons = _applicationDbContext.Coupons.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
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
                if (coupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon not found";
                }
                else
                {
                    _responseDto.Result = _mapper.Map<CouponDto>(coupon);
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpGet("GetByCode/{code}")]
        public ResponseDto GetCoupon(string code)
        {
            try
            {
                var coupon = _applicationDbContext.Coupons.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
                if (coupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon not found";
                }
                else
                {
                    _responseDto.Result = _mapper.Map<CouponDto>(coupon);
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto CreateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(coupon);
                _applicationDbContext.Coupons.Add(obj);
                _applicationDbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto UpdateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                _applicationDbContext.Entry(coupon).State = EntityState.Modified;
                _applicationDbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
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
