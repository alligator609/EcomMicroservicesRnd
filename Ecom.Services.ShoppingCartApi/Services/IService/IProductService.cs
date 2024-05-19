using Ecom.Services.ShoppingCartApi.Models.Dtos;

namespace Ecom.Services.ShoppingCartApi.Services.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts();
    }
}
