using AutoMapper;
using Ecom.MessageBus;
using Ecom.Services.OrderApi.Data;
using Ecom.Services.OrderApi.Models;
using Ecom.Services.OrderApi.Models.Dtos;
using Ecom.Services.OrderApi.Services.IService;
using Ecom.Services.OrderApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Ecom.Services.OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ResponseDto _responseDto;
        private IMapper _mapper;
        private IProductService _productService;
        private IMessageBus _messageBus;
        private IConfiguration _configuration;

        public OrderApiController(ApplicationDbContext applicationDbContext, IMapper mapper,
            IProductService productService, IMessageBus messageBus, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _productService = productService;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        //[Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = SD.Status_Pending;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);

                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);
                _applicationDbContext.OrderHeaders.Add(orderHeader);
                await _applicationDbContext.SaveChangesAsync();

                orderHeaderDto.Id = orderHeader.Id;
                _responseDto.Result = orderHeaderDto;
            }
            catch (Exception ex)
            {

                _responseDto.Result = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        } 
    }
}
