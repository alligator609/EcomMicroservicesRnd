using Ecom.Services.AuthApi.Models.Dtos;
using Ecom.Services.AuthApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Services.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ResponseDto _response;
        public AuthController(IAuthService service)
        {
            _service = service;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
        {
            var response = await _service.RegisterAsync(request);
            if (!string.IsNullOrEmpty(response))
            {
                _response.IsSuccess = false;
                _response.Message = response;
                return BadRequest(_response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var loginReponse = await _service.LoginAsync(request);
            if (loginReponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid credentials";
                return Unauthorized(_response);
            }
            _response.Result = loginReponse;
            return Ok(_response);
        }
    }
}
