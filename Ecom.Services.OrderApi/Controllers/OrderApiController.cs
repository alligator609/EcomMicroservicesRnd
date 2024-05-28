using AutoMapper;
using Ecom.MessageBus;
using Ecom.Services.OrderApi.Data;
using Ecom.Services.OrderApi.Models;
using Ecom.Services.OrderApi.Models.Dtos;
using Ecom.Services.OrderApi.Services.IService;
using Ecom.Services.OrderApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
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

        [Authorize]
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

        [Authorize]
        [HttpGet("CreateSriperSession")]
        public async Task<ResponseDto> CreateSriperSession([FromBody] StripeRequestDto stripeRequestDto)
        {
            try
            {             
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = stripeRequestDto.ApprovalUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,

                };
                foreach (var item in stripeRequestDto.OrderHeader.OrderDetails)
                {
                    var sessionItem = new Stripe.Checkout.SessionLineItemOptions
                    {
                        PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                            },
                            UnitAmount = (long)item.Product.Price * 100,
                        },
                        Quantity = item.Count,
                    };
                    options.LineItems.Add(sessionItem);
                }
                var service = new SessionService();
                Session session = service.Create(options);
                stripeRequestDto.SessionId = session.Id;
                stripeRequestDto.StripeSessionUrl = session.Url;
                OrderHeader orderHeader = await _applicationDbContext.OrderHeaders.FirstAsync(u => u.Id == stripeRequestDto.OrderHeader.Id);
                orderHeader.StripeSessionId = session.Url;
                await _applicationDbContext.SaveChangesAsync();
                _responseDto.Result = stripeRequestDto;
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
