using Api.Sales.Models.ValueObjects;

namespace Api.Sales.Models.DTOs.Requests
{
    public class RequestPurchaseDto
    {
        public List<Product>? CartProducts { get; set; }
        public double TotalAmount { get; set; }
    }
}
