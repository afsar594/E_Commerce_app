using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface ICart
    {
        Task<IEnumerable<Cart>> GetAllItemsAsync(); // Interface method declaration
        Task<Cart> GetItemByIdAsync(int id);

        // Task signature for handling a single Cart object
        Task<ServiceResponse<Cart>> AddItemAsync(Cart item);
        Task<Cart> UpdateItemAsync(Cart item);
        Task<bool> DeleteItemsAsync(int[] ids);
        //Task<List<Item>> GetItemByBrandName(string brandName);

        //Task<List<Item>> GetItemsByCategory(string CategoryName);




    }
}
