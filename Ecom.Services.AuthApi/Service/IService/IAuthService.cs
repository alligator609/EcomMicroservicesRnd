using Ecom.Services.AuthApi.Models.Dtos;

namespace Ecom.Services.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<string> RegisterAsync(RegistrationRequestDto request);
        Task<bool> AssaignRole(string email,string roleName);

    }
}
