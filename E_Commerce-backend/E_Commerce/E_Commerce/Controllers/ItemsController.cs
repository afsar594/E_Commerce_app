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
    public class ItemsController : ControllerBase
    {
        private readonly Iitem _itemsRepository;

        public ItemsController(Iitem itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> getAllitems()
        {
            //await Task.Delay(20000);
            var items = await _itemsRepository.GetAllItemsAsync(); // Await repository method
            return Ok(items); // Return the result as HTTP 200 OK
        }
     
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> getItemById(int id)
        {
            var item = await _itemsRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/items
        [HttpPost("SaveItem")]
        public async Task<ActionResult<Item>> CreateItem([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest(ModelState);
            }
            //await Task.Delay(10000);
            var createdItem = await _itemsRepository.AddItemAsync(item);
            return Ok(createdItem);
        }

        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _itemsRepository.UpdateItemAsync(item);
            return Ok(updatedItem);
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await _itemsRepository.DeleteItemAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("by-category/{categoryName}")]
        public async Task<ActionResult<Item>> GetItemCategory(string categoryName)  // Change 'brandName' to 'categoryName'
        {
            var items = await _itemsRepository.GetItemsByCategory(categoryName);  // Use 'categoryName' in the repository method
            if (items == null || !items.Any())  // Check for null or empty list
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("by-brand/{brandName}")]
        public async Task<ActionResult<List<Item>>> GetItemByBrandName(string brandName)
        {
            var items = await _itemsRepository.GetItemByBrandName(brandName);
            if (items == null || !items.Any())
            {
                return NotFound();
            }
            return Ok(items);
        }
        [HttpGet("category-brand")]
        public async Task<ActionResult<IEnumerable<BrandCategory>>> getAllbrandAndCategory()
        {
            //await Task.Delay(20000);
            var items = await _itemsRepository.getAllbrandAndCategory(); // Await repository method
            return Ok(items); // Return the result as HTTP 200 OK
        }


    }
}

