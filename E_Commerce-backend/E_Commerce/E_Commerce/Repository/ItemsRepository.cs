using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class ItemsRepository : Iitem
    {
        private readonly E_CommerceContext _context;

        public ItemsRepository(E_CommerceContext context)
        {
            _context = context;
        }

      


        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Set<Item>().FindAsync(id);
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            _context.Set<Item>().Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            _context.Set<Item>().Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.Set<Item>().FindAsync(id);
            if (item == null) return false;

            _context.Set<Item>().Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync(); // Correct usage of async with DbContext
        }



        public async Task<List<Item>> GetItemByBrandName(string brandName)
        {
            return await _context.Items
                                 .Where(i => i.Brand.Equals(brandName))
                                 .ToListAsync();
        }




        public async Task<List<Item>> GetItemsByCategory(string category)
        {
            return await _context.Items
                                 .Where(i => i.Category == category)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<BrandCategory>> getAllbrandAndCategory()
        {
           // return await _context.Items.ToListAsync(); // Correct usage of async with DbContext
            return await _context.Items
        .Select(item => new BrandCategory
        {
            Brand = item.Brand,
            Category = item.Category
        })
        .Distinct()
        .ToListAsync();
        }
    }
}
