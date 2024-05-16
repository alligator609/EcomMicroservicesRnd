namespace Ecom.Services.ShoppingCartApi.Models.Dtos
{
    public class CartHeaderDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal Discount { get; set; }
        public decimal CartTotal { get; set; }
    }
}
