using System.Collections.Generic;
using AutoMapper;
using Ecom.Services.ShoppingCartApi.Data;
using Ecom.Services.ShoppingCartApi.Models;
using Ecom.Services.ShoppingCartApi.Models.Dtos;
using Ecom.Services.ShoppingCartApi.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CartAPIController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;
    public ResponseDto _responseDto;
    private IMapper _mapper;
    private IProductService _productService;
    public CartAPIController(ApplicationDbContext applicationDbContext, IMapper mapper, IProductService productService)
    {
        _applicationDbContext = applicationDbContext;
        _responseDto = new ResponseDto();
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet("GetCart/{userId}")]
    public async Task<ResponseDto> RemoveCart(string userId)
    {
        try
        {
            var cartHeader = await  _applicationDbContext.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
            CartDto cartDto = new CartDto
            {
                CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
            };
            var cartDetails = _applicationDbContext.CartDetails.Where(x => x.CartHeaderId == cartHeader.Id);
            cartDto.CartDetails = (List<CartDetailsDto>)_mapper.Map<IList<CartDetailsDto>>(cartDetails);

            IEnumerable<ProductDto> products = await _productService.GetProducts();

            foreach (var cartDetail in cartDto.CartDetails)
            {
                cartDetail.Product = products.FirstOrDefault(x => x.Id == cartDetail.ProductId);
                cartDto.CartHeader.CartTotal += cartDetail.Count * cartDetail.Product.Price;
            }
            _responseDto.Result = cartDto;

        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }
    [HttpPost("CartUpsert")]
    public async Task<ResponseDto> CartUpsert(CartDto cartDto)
    {
        try
        {
            var cartHeader = _applicationDbContext.CartHeaders.FirstOrDefault(x => x.UserId == cartDto.CartHeader.UserId);
            if (cartHeader == null)
            {
                // create new cart header
                CartHeader cartHeaderMapped = _mapper.Map<CartHeader>(cartDto.CartHeader);
                _applicationDbContext.Add(cartHeaderMapped);
                await _applicationDbContext.SaveChangesAsync();

                cartDto.CartDetails.First().CartHeaderId = cartHeaderMapped.Id;
                _applicationDbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                await _applicationDbContext.SaveChangesAsync();

            }
            else
            {
                var cartDetailsFromDb = await _applicationDbContext.CartDetails.FirstOrDefaultAsync(
                    x => x.CartHeaderId == cartHeader.Id && x.ProductId == cartDto.CartDetails.First().ProductId);
                if (cartDetailsFromDb == null)
                {
                    // cart details
                    var newCartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    newCartDetails.CartHeaderId = cartHeader.Id;
                    _applicationDbContext.CartDetails.Add(newCartDetails);
                    await _applicationDbContext.SaveChangesAsync();
                }
                else
                {
                    // update count
                    cartDetailsFromDb.Count += cartDto.CartDetails.First().Count;
                    _applicationDbContext.CartDetails.Update(cartDetailsFromDb);
                    await _applicationDbContext.SaveChangesAsync();
                    cartDto.CartDetails[0] = _mapper.Map<CartDetailsDto>(cartDetailsFromDb);
                }
            }
            _responseDto.Result = cartDto;

        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }
    [HttpPost("RemoveCart")]
    public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
    {
        try
        {
            CartDetails cartDetails = _applicationDbContext.CartDetails.FirstOrDefault(x => x.Id == cartDetailsId);
            int totalCountOfCartItem = _applicationDbContext.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
            _applicationDbContext.CartDetails.Remove(cartDetails);

            if (totalCountOfCartItem == 1)
            {
                // remove cart header also cart details 
                var cartHeaderToremove = await _applicationDbContext.CartHeaders.FirstOrDefaultAsync(u => u.Id == cartDetails.CartHeaderId);

                _applicationDbContext.CartHeaders.Remove(cartHeaderToremove);
            }
            await _applicationDbContext.SaveChangesAsync();
            _responseDto.Result = true;

        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }

    [HttpPost("ApplyCoupon")]
    public async Task<ResponseDto> ApplyCoupon([FromBody] CartDto cartDto)
    {
        try
        {
            var cartFromDb = await _applicationDbContext.CartHeaders.FirstAsync(x=>x.UserId == cartDto.CartHeader.UserId);
            cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
            _applicationDbContext.Update(cartFromDb);
            await _applicationDbContext.SaveChangesAsync();
            _responseDto.Result = true;
        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }

    [HttpPost("RemoveCoupon")]
    public async Task<ResponseDto> RemoveCoupon([FromBody] CartDto cartDto)
    {
        try
        {
            var cartFromDb = await _applicationDbContext.CartHeaders.FirstAsync(x => x.UserId == cartDto.CartHeader.UserId);
            cartFromDb.CouponCode = "";
            _applicationDbContext.Update(cartFromDb);
            await _applicationDbContext.SaveChangesAsync();
            _responseDto.Result = true;
        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }
}
