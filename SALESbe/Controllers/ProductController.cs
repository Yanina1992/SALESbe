using Api.Sales.Models.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("GetProducts")]
        [ProducesResponseType(typeof(List<ResponseProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetProducts()
        {
            List<ResponseProductDto> response = new();

            //Input 1
            var book = new ResponseProductDto(1, "Book", 12.49m, true, false)
            {
                Name = "Book",
                ItemPrice = 12.49m,
                IsExempt = true,
                IsImported = false,
            };

            var musicCd = new ResponseProductDto(2, "Music CD", 14.99m, false, false)
            {
                Name = "Music CD",
                ItemPrice = 14.99m,
                IsExempt = false,
                IsImported = false,
            };

            var chocolateBar = new ResponseProductDto(3, "Chocolate bar", 0.85m, true, false)
            {
                Name = "Chocolate bar",
                ItemPrice = 0.85m,
                IsExempt = true,
                IsImported = false,
            };

            //Input 2
            var cheaperImportedChocolateBox = new ResponseProductDto(4, "Imported box of chocolates", 10.00m, true, true)
            {
                Name = "Imported box of chocolates",
                ItemPrice = 10.00m,
                IsExempt = true,
                IsImported = true,
            };

            var importedPerfumeBottle = new ResponseProductDto(5, "Imported bottle of perfume", 47.50m, false, true)
            {
                Name = "Imported bottle of perfume",
                ItemPrice = 47.50m,
                IsExempt = false,
                IsImported = true,
            };

            //Input 3
            var cheaperImportedPerfumeBottle = new ResponseProductDto(6, "Imported bottle of perfume", 27.99m, false, true)
            {
                Name = "Imported bottle of perfume",
                ItemPrice = 27.99m,
                IsExempt = false,
                IsImported = true,
            };

            var perfumeBottle = new ResponseProductDto(7, "Bottle of perfume", 18.99m, false, false)
            {
                Name = "Bottle of perfume",
                ItemPrice = 18.99m,
                IsExempt = false,
                IsImported = false,
            };

            var headachePills = new ResponseProductDto(8, "Packet of headache pills", 9.75m, true, false)
            {
                Name = "Packet of headache pills",
                ItemPrice = 9.75m,
                IsExempt = true,
                IsImported = false,
            };

            var importedChocolateBox = new ResponseProductDto(9, "Box of imported chocolates", 11.25m, true, true)
            {
                Name = "Box of imported chocolates",
                ItemPrice = 11.25m,
                IsExempt = true,
                IsImported = true,
            };

            response.Add(book);
            response.Add(musicCd);
            response.Add(chocolateBar);

            response.Add(cheaperImportedChocolateBox);
            response.Add(importedPerfumeBottle);

            response.Add(cheaperImportedPerfumeBottle);
            response.Add(perfumeBottle);
            response.Add(headachePills);
            response.Add(importedChocolateBox);

            return Ok(response);
        }
    }
}