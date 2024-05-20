using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Services.EmailApi.Models.Dtos
{
    public class CartDetailsDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        [NotMapped]
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
