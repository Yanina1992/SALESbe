using Api.Sales.Models;
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
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetProducts()
        {

            List<Product> response = new();

            //Input 1
            var book = new Product(1, 0, "Book", 12.49, 0, true, false)
            {
                Name = "Book",
                ItemPrice = 12.49,
                IsExempt = true,
                IsExported = false,
            };

            var musicCd = new Product(2, 0, "Music CD", 14.99, 0, false, false)
            {
                Name = "Music CD",
                ItemPrice = 14.99,
                IsExempt = false,
                IsExported = false,
            };

            var chocolateBar = new Product(3, 0, "Chocolate bar", 0.85, 0, true, false)
            {
                Name = "Chocolate bar",
                ItemPrice = 0.85,
                IsExempt = true,
                IsExported = false,
            };

            //Input 2
            var cheaperImportedChocolateBox = new Product(4, 0, "Imported box of chocolates", 10.00, 0, true, true)
            {
                Name = "Imported box of chocolates",
                ItemPrice = 10.00,
                IsExempt = true,
                IsExported = true,
            };

            var importedPerfumeBottle = new Product(5, 0, "Imported bottle of perfume", 47.50, 0, false, true)
            {
                Name = "Imported bottle of perfume",
                ItemPrice = 47.50,
                IsExempt = false,
                IsExported = true,
            };

            //Input 3
            var cheaperImportedPerfumeBottle = new Product(6, 0, "Imported bottle of perfume", 27.99, 0, false, true)
            {
                Name = "Imported bottle of perfume",
                ItemPrice = 27.99,
                IsExempt = false,
                IsExported = true,
            };

            var perfumeBottle = new Product(7, 0, "Bottle of perfume", 18.99, 0, false, false)
            {
                Name = "Bottle of perfume",
                ItemPrice = 18.99,
                IsExempt = false,
                IsExported = false,
            };

            var headachePills = new Product(8, 0, "Packet of headache pills", 9.75, 0, true, false)
            {
                Name = "Packet of headache pills",
                ItemPrice = 9.75,
                IsExempt = true,
                IsExported = false,
            };

            var importedChocolateBox = new Product(9, 0, "Box of imported chocolates", 11.25, 0, true, true)
            {
                Name = "Box of imported chocolates",
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
