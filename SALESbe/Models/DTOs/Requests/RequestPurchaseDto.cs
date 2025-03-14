using Api.Sales.Models.ValueObjects;

namespace Api.Sales.Models.DTOs.Requests
{
    public class RequestPurchaseDto
    {
        public int PurchaseId { get; set; }
        public List<Product>? CartProducts { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
