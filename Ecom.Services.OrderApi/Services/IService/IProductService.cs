using Ecom.Services.OrderApi.Models.Dtos;

namespace Ecom.Services.OrderApi.Services.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts();
    }
}
