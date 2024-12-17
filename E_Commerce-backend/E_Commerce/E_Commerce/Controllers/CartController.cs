using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Cors;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class CartController : ControllerBase
    {
        private readonly ICart _CartRepository;

        public CartController(ICart CartRepository)
        {
            _CartRepository = CartRepository;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> getAllitemsFromCart()
        {
            //await Task.Delay(20000);
            var items = await _CartRepository.GetAllItemsAsync(); // Await repository method
            return Ok(items); // Return the result as HTTP 200 OK
        }
     
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> getItemByIdFromCart(int id)
        {
            var item = await _CartRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        // POST: api/items/SaveItemAtCart
        [HttpPost("SaveItemAtCart")]
        public async Task<IActionResult> CreateItem([FromBody] Cart item)
        {
            if (item == null)
            {
                return BadRequest(new { message = "The cart item cannot be null." });
            }

            var result = await _CartRepository.AddItemAsync(item);

            if (!result.Success)
            {
               // return BadRequest(new { message = result.Message });
                return Ok(new { message = result.Message });

            }

            return Ok(new { message = result.Message, data = result.Data });
        }





        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAtCart(int id, [FromBody] Cart item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _CartRepository.UpdateItemAsync(item);
            return Ok(updatedItem);
        }

        // DELETE: api/items/{id}
        [HttpDelete]
        public async Task<IActionResult> DeleteItemsAtCart([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return BadRequest(new { message = "No IDs provided for deletion." });
            }

            var success = await _CartRepository.DeleteItemsAsync(ids);
            if (!success)
            {
                return NotFound(new { message = "One or more items not found." });
            }

            return Ok(new { message = "Items deleted successfully." });
        }

        //[HttpGet("by-category/{categoryName}")]
        //public async Task<ActionResult<Item>> GetItemCategory(string categoryName)  // Change 'brandName' to 'categoryName'
        //{
        //    var items = await _itemsRepository.GetItemsByCategory(categoryName);  // Use 'categoryName' in the repository method
        //    if (items == null || !items.Any())  // Check for null or empty list
        //    {
        //        return NotFound();
        //    }
        //    return Ok(items);
        //}

        //[HttpGet("by-brand/{brandName}")]
        //public async Task<ActionResult<List<Item>>> GetItemByBrandName(string brandName)
        //{
        //    var items = await _itemsRepository.GetItemByBrandName(brandName);
        //    if (items == null || !items.Any())
        //    {
        //        return NotFound();
        //    }
        //    return Ok(items);
        //}


    }
}

