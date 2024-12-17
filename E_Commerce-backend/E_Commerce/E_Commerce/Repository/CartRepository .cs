using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class CartRepository : ICart
    {
        private readonly E_CommerceContext _context;

        public CartRepository(E_CommerceContext context)
        {
            _context = context;
        }




        public async Task<ServiceResponse<Cart>> AddItemAsync(Cart item)
        {
            if (item == null)
            {
                return new ServiceResponse<Cart>
                {
                    Success = false,
                    Message = "Invalid cart item.",
                    Data = null
                };
            }

            var existingItem = await _context.Set<Cart>().FirstOrDefaultAsync(c => c.itemId == item.itemId);

            if (existingItem != null)
            {
                return new ServiceResponse<Cart>
                {
                    Success = false,
                    Message = "This item already exists.",
                    Data = null
                };
            }

            await _context.Set<Cart>().AddAsync(item);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Cart>
            {
                Success = true,
                Message = "Item added successfully.",
                Data = item
            };
        }





        public async Task<Cart> UpdateItemAsync(Cart item)
        {
            _context.Set<Cart>().Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteItemsAsync(int[] ids)
        {
            var items = await _context.Set<Cart>().Where(c => ids.Contains(c.Id)).ToListAsync();
            if (items.Count != ids.Length)
            {
                return false; // Some items were not found
            }

            _context.Set<Cart>().RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<IEnumerable<Cart>> GetAllItemsAsync()
        {
            return await _context.Carts.ToListAsync(); // Correct usage of async with DbContext
        }

        public Task<Cart> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }



        //public async Task<List<Item>> GetItemByBrandName(string brandName)
        //{
        //    return await _context.Items
        //                         .Where(i => i.Brand.Equals(brandName))
        //                         .ToListAsync();
        //}




        //public async Task<List<Item>> GetItemsByCategory(string category)
        //{
        //    return await _context.Items
        //                         .Where(i => i.Category == category)
        //                         .ToListAsync();
        //}
    }
}
