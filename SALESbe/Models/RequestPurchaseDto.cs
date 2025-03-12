namespace Api.Sales.Models
{
    public class RequestPurchaseDto
    {
        public required List<Product> Products { get; set; }
    }
}
