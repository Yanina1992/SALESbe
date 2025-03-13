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

        public RequestPurchaseDto Purchase = new RequestPurchaseDto();

        [HttpGet]
        [Route("GetPurchases")]
        [ProducesResponseType(typeof(List<ResponsePurchaseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public IActionResult GetPurchases([FromQuery] string ?purchaseId)
        {
            //response.PurchaseId = new Random().Next(1, 1000);

            List<ResponsePurchaseDto> response = new();

            try
            {
                //
                var products = this.Purchase.CartProducts;
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
                this.Purchase = request;

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

    }
}
