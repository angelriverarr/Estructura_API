using Microsoft.AspNetCore.Mvc;
using Estructura_API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Estructura_API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 100.00M },
            new Product { Id = 2, Name = "Product 2", Price = 200.00M }
        };

        // Obtener: api/product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(products);
        }

        // obtener: api/product/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // peticion POST: api/product
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // peticion PUT: api/product/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return NoContent();
        }

        // Eliminar: api/product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return NoContent();
        }
    }
}