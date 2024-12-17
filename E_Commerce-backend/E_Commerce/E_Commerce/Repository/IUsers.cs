using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IUsers
    {
        Task<IEnumerable<Users>> GetAllUsersAsync(); // Interface method declaration
        Task<Users> GetUserByCredentialsAsync(string email, string password);

        Task<Users> AddUsersAsync(Users Users);
        Task<Users> UpdateUsersAsync(Users Users);
      //  Task<Users> GetUserByEmail(string email);
        Task<bool> DeleteItemAsync(int id);
    }
}
