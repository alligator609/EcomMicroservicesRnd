using System;
using System.Reflection.PortableExecutable;
using AutoMapper;
using Ecom.Services.ShoppingCartApi.Data;
using Ecom.Services.ShoppingCartApi.Models;
using Ecom.Services.ShoppingCartApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CartAPIController :ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;
    public ResponseDto _responseDto;
    private IMapper _mapper;
    public CartAPIController(ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _responseDto = new ResponseDto();
        _mapper = mapper;
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
                    x => x.CartHeaderId == cartHeader.Id && x.ProductId == cartDto.CartDetails.First().ProductId );
                if(cartDetailsFromDb == null)
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
}
