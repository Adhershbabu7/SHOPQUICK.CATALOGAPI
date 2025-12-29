using Microsoft.AspNetCore.Mvc;
using SHOPQUICK.CATALOGAPI.Models;

namespace SHOPQUICK.CATALOGAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public static readonly List<Product> _products = new()
        {
            new Product { id = 1, name = "Iphone", description = "new phone", price = 100 },
            new Product { id = 2, name = "ephone", description = "new phone", price = 100 },
            new Product { id = 3, name = "bphone", description = "new phone", price = 100 }
        };

        // GET: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_products);
        }

        // GET: api/Product/1
        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            Product found = null;

            foreach (Product p in _products)
            {
                if (p.id == id)
                {
                    found = p;
                    break;
                }
            }

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult<Product>NewPrdct(Product product)
        {
            int maxId = 0;
            
            foreach (Product p in _products)
            {
                if (p.id > maxId)
                    maxId = p.id;
            }

            product.id = maxId + 1;
            _products.Add(product);

            return CreatedAtAction(nameof(Get), new { id = product.id }, product);
        }

        // PUT: api/Product/1
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, Product update)
        {
            Product existing = null;

            foreach (Product p in _products)
            {
                if (p.id == id)
                {
                    existing = p;
                    break;
                }
            }

            if (existing == null)
                return NotFound();

            existing.name = update.name;
            existing.description = update.description;
            existing.price = update.price;

            return NoContent();
        }

        // DELETE: api/Product/1
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            Product existing = null;

            foreach (Product p in _products)
            {
                if (p.id == id)
                {
                    existing = p;
                    break;
                }
            }

            if (existing == null)
                return NotFound();

            _products.Remove(existing);
            return NoContent();
        }
    }
}
