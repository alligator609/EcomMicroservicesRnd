using Ecom.Services.EmailApi.Models.Dtos;

namespace Ecom.Services.EmailApi.Services
{
    public interface IEmailServicecs
    {
        public Task EmailCartLod(CartDto cartDto);
    }
}
