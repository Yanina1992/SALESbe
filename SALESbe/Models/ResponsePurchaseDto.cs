namespace SALESbe.Models
{
    public class ResponsePurchaseDto
    {
        public required int PurchaseId { get; set; }
        public required List<Product> ?Products { get; set; }
        public required decimal TotalCost { get; set; }
        public required decimal TotalTaxes { get; set; }
    }
}
