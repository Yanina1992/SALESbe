namespace Api.Sales.Models.ValueObjects
{
    public class Product
    {
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public required string Name { get; set; }
        public required decimal ItemPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public required bool IsExempt { get; set; }
        public required bool IsExported { get; set; }

        public Product() { }

        public Product(int productId, int quantity, string name, decimal itemPrice, decimal totalPrice, bool isExempt, bool isExported)
        {
            ProductId = productId;
            Quantity = quantity;
            Name = name;
            ItemPrice = itemPrice;
            TotalPrice = totalPrice;
            IsExempt = isExempt;
            IsExported = isExported;
        }

    }
}
