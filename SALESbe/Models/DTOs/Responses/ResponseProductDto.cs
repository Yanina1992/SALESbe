﻿namespace Api.Sales.Models.DTOs.Responses
{
    public class ResponseProductDto
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required decimal ItemPrice { get; set; }
        public required bool IsExempt { get; set; }
        public required bool IsExported { get; set; }

        public ResponseProductDto(int productId, string name, decimal itemPrice, bool isExempt, bool isExported)
        {
            ProductId = productId;
            Name = name;
            ItemPrice = itemPrice;
            IsExempt = isExempt;
            IsExported = isExported;
        }

    }
}