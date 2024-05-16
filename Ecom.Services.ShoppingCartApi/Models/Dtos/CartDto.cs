namespace Ecom.Services.ShoppingCartApi.Models.Dtos
{
    public class CartDto
    {
        public CartHeaderDto CartHeaderDto { get; set; }
        public List<CartDetailsDto> CartDetailsDto { get; set; }
    }
}
