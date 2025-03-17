using Api.Sales.Models.DTOs.Requests;
using Api.Sales.Models.DTOs.Responses;
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

        public IActionResult GetPurchase([FromQuery] string? purchaseId)
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
                        decimal isImportedTaxesToAdd = 0;

                        if (!product.IsExempt && !product.IsImported)
                        {
                            product.TotalPrice = 0;

                            var basicSaleTaxe = (product.ItemPrice * 10) / 100;

                            var roundedBasicSaleTaxe = this.RoundNumbersUp(basicSaleTaxe);
                            isNotExemptItemTaxesToAdd = roundedBasicSaleTaxe;
                            //
                            sumDifferentTaxes += Convert.ToDecimal(isNotExemptItemTaxesToAdd * product.Quantity);

                            product.TotalPrice = (isNotExemptItemTaxesToAdd * product.Quantity + product.ItemPrice * product.Quantity);
                        }

                        if (product.IsImported && product.IsExempt)
                        {
                            product.TotalPrice = 0;

                            var importDuty = (product.ItemPrice * 5) / 100;
                            var roundedImportDuty = this.RoundNumbersUp(importDuty);
                            isImportedTaxesToAdd = roundedImportDuty;
                            sumDifferentTaxes += Convert.ToDecimal(isImportedTaxesToAdd * product.Quantity);

                            product.TotalPrice = (isImportedTaxesToAdd * product.Quantity + product.ItemPrice * product.Quantity);
                        }

                        if (!product.IsExempt && product.IsImported) 
                        {
                            product.TotalPrice = 0;

                            var allTaxes = (product.ItemPrice * 15) / 100;
                            var allTaxesRounded = this.RoundNumbersUp(allTaxes);
                            sumDifferentTaxes += Convert.ToDecimal(allTaxesRounded * product.Quantity);
                            
                            product.TotalPrice = (allTaxesRounded * product.Quantity + product.ItemPrice * product.Quantity);
                        }
                    }

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

            //Handle numbers with comma
            if (numberToRound.ToString().Length > 2)
            {

                var splittedNumber = numberToRound.ToString().Split(',');

                var numbersBeforeComma = splittedNumber[0];

                var numbersAfterComma = splittedNumber[1];

                //2 numbers or more after comma
                if (numbersAfterComma.Length > 1)

                {
                    var firstNumberAfterComma = numbersAfterComma[0];

                    var secondNumberAfterComma = numbersAfterComma[1];

                    var convertedFirstNumberAfterComma = decimal.Parse(firstNumberAfterComma.ToString());

                    var convertedSecondNumberAfterComma = decimal.Parse(secondNumberAfterComma.ToString());


                    if(convertedSecondNumberAfterComma == 5)
                    {
                        string roundedNumber = numbersBeforeComma + ',' + convertedFirstNumberAfterComma + convertedSecondNumberAfterComma;
                        decimal recomposedNumber = Convert.ToDecimal(roundedNumber);

                        return recomposedNumber;
                    }
                    if(convertedSecondNumberAfterComma < 5)
                    {
                        convertedSecondNumberAfterComma = 5;

                        string roundedNumber = numbersBeforeComma + ',' + convertedFirstNumberAfterComma + convertedSecondNumberAfterComma;
                        decimal recomposedNumber = Convert.ToDecimal(roundedNumber);
                        
                        return recomposedNumber;
                    }
                    if(convertedSecondNumberAfterComma > 5)
                    {
                        convertedSecondNumberAfterComma = 0;
                        convertedFirstNumberAfterComma ++;

                        string roundedNumber = numbersBeforeComma + ',' + convertedFirstNumberAfterComma + convertedSecondNumberAfterComma;
                        decimal recomposedNumber = Convert.ToDecimal(roundedNumber);

                        return recomposedNumber;
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
