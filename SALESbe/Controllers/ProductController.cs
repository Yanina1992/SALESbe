using Microsoft.AspNetCore.Mvc;
using SALESbe.Models;
using System.Net;

namespace SALESbe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("/[action]")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetProducts()
        {

            List<Product> response = new ();

            //Input 1
            var book = new Product(1, 0, "book", 12.49, 0, true, false)
            {
                Name = "book",
                ItemPrice = 12.49,
                IsExempt = true,
                IsExported = false,
            };

            var musicCd = new Product(2, 0, "musicCd", 14.99, 0, false, false)
            {
                Name = "musicCd",
                ItemPrice = 14.99,
                IsExempt = false,
                IsExported = false,
            };

            var chocolateBar = new Product(3, 0, "chocolateBar", 0.85, 0, true, false)
            {
                Name = "chocolateBar",
                ItemPrice = 0.85,
                IsExempt = true,
                IsExported = false,
            };

            //Input 2
            var cheaperImportedChocolateBox = new Product(4, 0, "cheaperImportedChocolateBox", 10.00, 0, true, true)
            {
                Name = "cheaperImportedChocolateBox",
                ItemPrice = 10.00,
                IsExempt = true,
                IsExported = true,
            };

            var importedPerfumeBottle = new Product(5, 0, "importedPerfumeBottle", 47.50, 0, false, true)
            {
                Name = "importedPerfumeBottle",
                ItemPrice = 47.50,
                IsExempt = false,
                IsExported = true,
            };

            //Input 3
            var cheaperImportedPerfumeBottle = new Product(6, 0, "cheaperImportedPerfumeBottle", 27.99, 0, false, true)
            {
                Name = "cheaperImportedPerfumeBottle",
                ItemPrice = 27.99,
                IsExempt = false,
                IsExported = true,
            };

            var perfumeBottle = new Product(7, 0, "perfumeBottle", 18.99, 0, false, false)
            {
                Name = "perfumeBottle",
                ItemPrice = 18.99,
                IsExempt = false,
                IsExported = false,
            };

            var headachePills = new Product(8, 0, "headachePills", 9.75, 0, true, false)
            {
                Name = "headachePills",
                ItemPrice = 9.75,
                IsExempt = true,
                IsExported = false,
            };

            var importedChocolateBox = new Product(9, 0, "importedChocolateBox", 11.25, 0, true, true)
            {
                Name = "importedChocolateBox",
                ItemPrice = 11.25,
                IsExempt = true,
                IsExported = false,
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
