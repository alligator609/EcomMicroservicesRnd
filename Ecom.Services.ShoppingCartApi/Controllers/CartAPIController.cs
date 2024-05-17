using System;
using AutoMapper;
using Ecom.Services.ShoppingCartApi.Data;
using Ecom.Services.ShoppingCartApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

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
            //Cart cart = _mapper.Map<Cart>(cartDto);
            //_responseDto.Result = _mapper.Map<CartDto>(cart);
        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }
}
