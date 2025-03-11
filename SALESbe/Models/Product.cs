namespace SALESbe.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int ?Quantity { get; set; }
        public required string Name { get; set; }
        public required double ItemPrice { get; set; }
        public double ?TotalPrice { get; set; }
        public required bool IsExempt { get; set; }
        public required bool IsExported { get; set; }

    public Product(int productId, int quantity, string name, double itemPrice, double totalPrice, bool isExempt, bool isExported) 
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
