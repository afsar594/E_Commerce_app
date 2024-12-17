using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface Iitem
    {
        Task<IEnumerable<Item>> GetAllItemsAsync(); // Interface method declaration
        Task<Item> GetItemByIdAsync(int id);

        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
        Task<List<Item>> GetItemByBrandName(string brandName);

        Task<List<Item>> GetItemsByCategory(string CategoryName);
        Task<IEnumerable<BrandCategory>> getAllbrandAndCategory(); // Interface method declaration





    }
}
