using Api.Sales.Models.ValueObjects;

namespace Api.Sales.Models.DTOs.Responses
{
    public class ResponsePurchaseDto
    {
        public required int PurchaseId { get; set; }
        public required List<Product>? Products { get; set; }
        public required decimal SalesTaxes { get; set; }
        public required decimal Total { get; set; }
    }
}