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
        [Route("/[action]")]
        [ProducesResponseType(typeof(List<ResponsePurchaseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public IActionResult GetPurchase([FromQuery] string ?purchaseId)
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


/**
 
 try
            {
                var query = new GetAlimentazioneQuery(0, 500, "ASC", "NomeAlimentazione", simpleFilter: nomeAlimentazione);
                var result = await _mediator.Send(query);
                if (result != null)
                {
                    if (result.Alimentazioni != null)
                    {
                        responses = result.Alimentazioni.Select(m => new ResponseAutocompleteDto
                        {
                            Value = m.IdAlimentazione,
                            Name = m.NomeAlimentazione
                        }
                        ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);

            }
            return Ok(responses);

 */