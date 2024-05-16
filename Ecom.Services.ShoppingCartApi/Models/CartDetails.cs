using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecom.Services.ShoppingCartApi.Models.Dtos;

namespace Ecom.Services.ShoppingCartApi.Models
{
    public class CartDetails
    {
        [Key]
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }
        
    }
}
