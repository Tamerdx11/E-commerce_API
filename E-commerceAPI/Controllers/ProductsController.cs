using E_commerceAPI.Models;
using E_commerceAPI.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct _product;

        public ProductsController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("all")]
        [ResponseCache(Duration = 120)]
        [Authorize(Roles = "User, Admin")]
        public ActionResult<IEnumerable<Product>> Index()
        {
            return Ok(_product.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var product = await _product.GetById(id);

            if (product is null)
                return NotFound();

            return product;
        }

        [HttpGet("Search")]
        [Authorize(Roles = "User, Admin")]
        [ResponseCache(Duration = 120)]
        public async Task<List<Product>> GetProductByName(string? name = null, string? category = null)
        {
            return await _product.GetByNameAndCategory(name!, category!);
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] ProductDto productDto)
        {
            if (productDto != null)
            {
                Product product = new Product
                {
                    Name = productDto.Name,
                    Category = productDto.Category,
                    Description = productDto.Description,
                    ImageUrl = productDto.ImageUrl,
                    Price = productDto.Price
                };
                await _product.Add(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            return BadRequest();
        }


        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult UpdateProduct([FromRoute] int id, [FromBody] ProductDto productDto)
        {
            _product.Update(id, productDto);

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            _product.Delete(id);

            return NoContent();
        }



    }
}
