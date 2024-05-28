namespace Ecom.Services.OrderApi.Models.Dtos
{
    public class StripeRequestDto
    {
        public string ApprovalUrl { get; set; }
        public string CancelUrl { get; set; }
        public OrderHeaderDto OrderHeader { get; set; }
        public string StripeSessionUrl { get; internal set; }
        public string SessionId { get; internal set; }
    }
}
