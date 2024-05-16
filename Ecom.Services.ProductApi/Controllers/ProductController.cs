using AutoMapper;
using Ecom.Services.ProductApi.Data;
using Ecom.Services.ProductApi.Models;
using Ecom.Services.ProductApi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ResponseDto _responseDto;
        private IMapper _mapper;
        public ProductController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto GetProduct()
        {
            try
            {
                IEnumerable<Product> products = _applicationDbContext.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpGet("{id}")]
        public ResponseDto GetProduct(int id)
        {
            try
            {
                var product = _applicationDbContext.Products.FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon not found";
                }
                else
                {
                    _responseDto.Result = _mapper.Map<ProductDto>(product);
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
        public ResponseDto CreateProduct([FromBody] Product product)
        {
            try
            {
                Product obj = _mapper.Map<Product>(product);
                _applicationDbContext.Products.Add(obj);
                _applicationDbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<ProductDto>(obj);
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
        public ResponseDto UpdateProduct([FromBody] Product product)
        {
            try
            {
                _applicationDbContext.Entry(product).State = EntityState.Modified;
                _applicationDbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<ProductDto>(product);
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
                var product = _applicationDbContext.Products.FirstOrDefault(x => x.Id == id);
                _applicationDbContext.Products.Remove(product);
                _applicationDbContext.SaveChanges();
                _responseDto.Result = product;
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
