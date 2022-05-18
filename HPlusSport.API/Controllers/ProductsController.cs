using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // create a constructor and set Database context as it's injected

        private readonly ShopContext _context;
        public ProductsController(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }



        // action methods
        [HttpGet]  // be explicit  -- this is a missing ingredient which is routing
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToArrayAsync());
        }

        //  [HttpGet, Route("/products/{id}")]  .... OR just put it into the HttpGet call
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            /*  if (!ModelState.IsValid)
            {
                return BadRequest();  // this would be a good place for custom error handling
            } 
            now handled in Program.Cs
            */

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(  // helper method
                "GetProduct",   // the name of the method that is responsible for retrieving 
                new
                {
                    id = product.Id  // anonymous object of id that was just stored in the database
                },
                product);   // the product itself

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // two possible problems - one is that it was changed, one is that it was deleted
                if (!_context.Products.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }


        /* public IEnumerable<Product> GetAllProducts()
        {
           return _context.Products.ToArray();
        }*/



    }
}
