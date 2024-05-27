﻿namespace Ecom.Services.OrderApi.Models.Dtos
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
