using Api.Sales.Models.DTOs.Requests;
using Api.Sales.Models.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SALESbe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        public static RequestPurchaseDto Purchase = new RequestPurchaseDto();

        [HttpGet]
        [Route("GetPurchase")]
        [ProducesResponseType(typeof(ResponsePurchaseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public IActionResult GetPurchase([FromQuery] string ?purchaseId)
        {

            ResponsePurchaseDto response; 

            double sumTaxes = 0;

            try
            {
                var products = PurchaseController.Purchase.CartProducts;
                
                if (products != null)
                {

                    foreach (var product in products)
                    {
                        //da aggiungere a ItemPrice prima di generare lo scontrino
                        double isNotExemptItemTaxesToAdd = 0;
                        double isExportedTaxesToAdd = 0;

                        //da aggiungere a TotalPrice prima di generare lo scontrino
                        double isNotExemptitemTotalTaxesToAdd = 0;
                        double isExportedTotalTaxesToAdd = 0;

                        if (!product.IsExempt)
                        {
                            var roundedNumber = this.RoundNumbersUp(product.ItemPrice);
                            var basicSaleTaxe = (roundedNumber * 10) / 100;

                            //product.ItemPrice += basicSaleTaxe;
                            isNotExemptItemTaxesToAdd = basicSaleTaxe;

                            //isNotExemptitemTotalTaxesToAdd = roundedNumber;

                            product.ItemPrice = roundedNumber;

                            roundedNumber = Convert.ToDouble(basicSaleTaxe * product.Quantity);
                            
                            
                            //?
                            //sumTaxes += basicSaleTaxe;
                        }

                        if (product.IsExported)
                        {
                            double roundedNumber = product.ItemPrice;

                            if (product.ItemPrice.ToString().Contains(','))
                            {
                                roundedNumber = this.RoundNumbersUp(product.ItemPrice);
                            }
 
                            var importDuty = (roundedNumber * 5) / 100;

                            isExportedTaxesToAdd = importDuty;

                            isExportedTotalTaxesToAdd = Convert.ToDouble(importDuty * product.Quantity);

                            //?
                            //sumTaxes += importDuty;
                        }

                        if (isNotExemptItemTaxesToAdd > 0 || isExportedTaxesToAdd > 0)
                        {
                            product.ItemPrice += (isNotExemptItemTaxesToAdd + isExportedTaxesToAdd);
                        }

                        if (isNotExemptItemTaxesToAdd == 0 && isExportedTaxesToAdd == 0)
                        {
                            product.ItemPrice = this.RoundNumbersUp(product.ItemPrice);
                        }
                        
                        product.TotalPrice = product.ItemPrice * product.Quantity;
                    }
                }
                response = new ResponsePurchaseDto()
                {
                    PurchaseId = PurchaseController.Purchase.PurchaseId,
                    Products = PurchaseController.Purchase.CartProducts,
                    SalesTaxes = 0,
                    Total = Purchase.TotalAmount
                };

                var sumTaxesString = sumTaxes.ToString().Split(',');
                var numbersAfterComma = sumTaxesString[1];
                var numbersAfterCommaLength = numbersAfterComma.Length;

                if (numbersAfterCommaLength > 1)
                {
                    response.SalesTaxes = this.RoundNumbersUp(sumTaxes);
                    response.Total = PurchaseController.Purchase.TotalAmount + response.SalesTaxes;
                }
                else
                {
                    response.SalesTaxes = sumTaxes;
                    response.Total = PurchaseController.Purchase.TotalAmount + sumTaxes;
                }
                
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(response);
    
        }

        [HttpPost]
        [Route("InsertPurchase")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public IActionResult InsertPurchase([FromBody] RequestPurchaseDto request)
        {
            ResponseDto response = new();

            try
            {
                PurchaseController.Purchase = request;
                PurchaseController.Purchase.PurchaseId = new Random().Next(1, 1000);

                response.RowsAffected = 1;
                response.Message = "Purchase saved";
            }
            catch (Exception ex)
            {
                response.RowsAffected = 0;
                response.Errors = "Error";

                return StatusCode(500, ex.Message);
            }
            return Ok(response);

        }


        private double RoundNumbersUp(double numberToRound)
        {

            var splittedNumber = numberToRound.ToString().Split(',');

            var numbersBeforeComma = splittedNumber[0];

            var numbersAfterComma = splittedNumber[1];

            if (numbersAfterComma.Length > 1)

            {
                var firstNumberAfterComma = numbersAfterComma[0];

                var  secondNumberAfterComma = numbersAfterComma[1];

                var convertedSecondNumberAfterComma1 = double.Parse(firstNumberAfterComma.ToString());

                var convertedSecondNumberAfterComma2 = double.Parse(secondNumberAfterComma.ToString());

                if (convertedSecondNumberAfterComma1 > 5 && convertedSecondNumberAfterComma2 > 5 && convertedSecondNumberAfterComma2 <= 9)
                {
                    var roundedNumber = (int)Math.Ceiling(numberToRound);

                    return roundedNumber;
                }
                else if (convertedSecondNumberAfterComma1 < 5 && convertedSecondNumberAfterComma2 > 5 && convertedSecondNumberAfterComma2 <= 9)
                {
                    var roundedNumber = 5;

                    string recomposedNumber = numbersBeforeComma + numbersAfterComma[0] + roundedNumber;
                    double parsedRecomposedNumber = Convert.ToDouble(recomposedNumber);

                    return parsedRecomposedNumber;

                }
                else if (convertedSecondNumberAfterComma2 >= 1 && convertedSecondNumberAfterComma2 < 5)
                {
                    var roundedNumber = 5;

                    string recomposedNumber = numbersBeforeComma + numbersAfterComma[0] + roundedNumber;
                    double parsedRecomposedNumber = Convert.ToDouble(recomposedNumber);

                    return parsedRecomposedNumber;

                }
               
            }
            return numberToRound;
        }
    }
}
