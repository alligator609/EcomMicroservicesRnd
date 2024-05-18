using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Services.ShoppingCartApi.Models.Dtos
{
    public class CartDetailsDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        [NotMapped]
        public CartHeader? CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
