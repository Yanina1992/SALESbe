using Api.Sales.Models.DTOs.Requests;
using Api.Sales.Models.DTOs.Responses;
using Api.Sales.Models.ValueObjects;
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

            decimal sumDifferentTaxes = 0;
            

            try
            {
                var products = PurchaseController.Purchase.CartProducts;


                //Response

                response = new ResponsePurchaseDto()
                {
                    PurchaseId = PurchaseController.Purchase.PurchaseId,
                    Products = PurchaseController.Purchase.CartProducts,
                    SalesTaxes = 0,
                    Total = Purchase.TotalAmount
                };


                if (products != null)
                {
                    //Handle products
                    foreach (var product in products)
                    {

                        decimal isNotExemptItemTaxesToAdd = 0;
                        decimal isExportedTaxesToAdd = 0;

                        if (!product.IsExempt)
                        {
                            product.TotalPrice = 0;

                            var basicSaleTaxe = (product.ItemPrice * 10) / 100;

                            var roundedBasicSaleTaxe = this.RoundNumbersUp(basicSaleTaxe);
                            isNotExemptItemTaxesToAdd = roundedBasicSaleTaxe;
                            sumDifferentTaxes += isNotExemptItemTaxesToAdd;

                            product.TotalPrice = (sumDifferentTaxes * product.Quantity + product.ItemPrice * product.Quantity);
                        }

                        if (product.IsExported)
                        {
                            product.TotalPrice = 0;
 
                            var importDuty = (product.ItemPrice * 5) / 100;

                            this.RoundNumbersUp(importDuty);
                            isExportedTaxesToAdd = importDuty;
                            sumDifferentTaxes += isExportedTaxesToAdd;

                            //product.TotalPrice += (sumDifferentTaxes * product.Quantity);
                            product.TotalPrice = (sumDifferentTaxes * product.Quantity + product.ItemPrice * product.Quantity);
                        }

                    }

                       var purchaseSaleTaxRounded = this.RoundNumbersUp(sumDifferentTaxes);
                       response.SalesTaxes = purchaseSaleTaxRounded;
                       response.Total = purchaseSaleTaxRounded;

                        
                }

                 response.SalesTaxes = sumDifferentTaxes;
                 response.Total = PurchaseController.Purchase.TotalAmount + sumDifferentTaxes;
                
                
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


        private decimal RoundNumbersUp(decimal numberToRound)
        {
            //numberToRound > input ************ //numberToReturn > output

            //Handle numbers with comma
            if (numberToRound.ToString().Length > 2) {

                var splittedNumber = numberToRound.ToString().Split(',');

                var numbersBeforeComma = splittedNumber[0];

                var numbersAfterComma = splittedNumber[1];

                //2 numbers after comma
                if (numbersAfterComma.Length > 1)

                {
                    var firstNumberAfterComma = numbersAfterComma[0];

                    var secondNumberAfterComma = numbersAfterComma[1];

                    var convertedFirstNumberAfterComma = decimal.Parse(firstNumberAfterComma.ToString());

                    var convertedSecondNumberAfterComma = decimal.Parse(secondNumberAfterComma.ToString());

                    if (convertedFirstNumberAfterComma <= 4 && convertedSecondNumberAfterComma >= 9)
                    {
                        var roundedNumber = 5;

                        string recomposedNumber = numbersBeforeComma + ',' + roundedNumber;
                        decimal parsedRecomposedNumber = Convert.ToDecimal(recomposedNumber);

                        return parsedRecomposedNumber;
                    }

                    if (convertedFirstNumberAfterComma >= 5 && convertedSecondNumberAfterComma >= 1)
                    {

                        var roundedNumber = (int)Math.Ceiling(numberToRound);

                        return roundedNumber;

                    }

                }





                //1 number after comma
                if (numbersAfterComma.Length == 1)
                {
                    var parsedNumbersAfterComma = Convert.ToDecimal(numbersAfterComma);

                    //If first number after comma < 5 it becomes == 5
                    if (parsedNumbersAfterComma <= 5)
                    {
                        string numberToReturn = Convert.ToString(numbersBeforeComma);
                        numberToReturn = numbersBeforeComma + ",5";

                        return Convert.ToDecimal(numberToReturn);
                    }
                    //If first number after comma > 5 it will be rounded up by Math.Ceiling
                    else
                    {
                        var numberToReturn = (int)Math.Ceiling(numberToRound);

                        return Convert.ToDecimal(numberToReturn);
                    }
                }          

            }
            //Numbers without comma
            else
            {
                return numberToRound;
            }

            return numberToRound;
        }


    }
}
