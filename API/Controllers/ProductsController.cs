
using API.Data;
using API.Entities;
using API.RequestHelpers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Diagnostics;

namespace API.Controllers
{
    public class ProductsController: BaseApiController
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams){
            var query = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .Filter(productParams.Brands, productParams.Types)
                .AsQueryable();

            
            var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber,
             productParams.PageSize);

            Trace.WriteLine(products);

            Response.AddPaginationHeader(products.MetaData);
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            var product = await _context.Products.FindAsync(id);
            if(product == null) return NotFound();
            return product;
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters(){
            var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
            return Ok(new {brands, types});
        }

        
       /* [HttpPost()]
        public async Task<ActionResult<Product>> GetProduct(Product newProduct){
            return await _context.Products.AddAsync(newProduct);
        }*/

    }
}