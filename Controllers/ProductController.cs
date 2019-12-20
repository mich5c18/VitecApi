using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VitecApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VitecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ModelContext _context;

        public ProductController(ModelContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                // Create a new Product if collection is empty,
                // which means you can't delete all Products.
                _context.Products.Add(new Product { Name = "BestProduct", Price = 50, Description = "The best product you will ever find" });
                _context.Products.Add(new Product { Name = "NextBestProduct", Price = 80, Description = "Nothing to see here"});
                _context.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product item)
        {
            _context.Products.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { /*Name = */item.Name, Price = item.Price, Description = item.Description}, item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product item)
        {
            //var productItem = await _context.Products.FindAsync(id);

            //if (productItem == null)
            //{
            //    return NotFound();
            //}

            //_context.Entry(item).State = EntityState.Modified;
            //await _context.SaveChangesAsync();

            //return NoContent();

            if (id != item.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productItem = await _context.Products.FindAsync(id);

            if (productItem == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
