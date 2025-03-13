using Api.Sales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SALESbe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        [HttpGet]
        [Route("GetPurchase")]
        [ProducesResponseType(typeof(List<ResponsePurchaseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public IActionResult GetPurchases([FromQuery] string ?purchaseId)
        {
            List<ResponsePurchaseDto> response = new();

            try
            {
                
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(response);
    
        }

        [HttpPost]
        [Route("InsertPurchase")]
        [ProducesResponseType(typeof(List<RequestPurchaseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public IActionResult InsertPurchase([FromBody] RequestPurchaseDto request)
        {
            List<ResponsePurchaseDto> response = new();

            try
            {

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(response);

        }

    }
}
