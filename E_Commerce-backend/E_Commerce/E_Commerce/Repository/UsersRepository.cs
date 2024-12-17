using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class UsersRepository : IUsers
    {
        private readonly E_CommerceContext _context;

        public UsersRepository(E_CommerceContext context)
        {
            _context = context;
        }




        public async Task<Users> GetUserByCredentialsAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }


        public async Task<Users> AddUsersAsync(Users user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception($"Could not add user to the database: {ex.InnerException.Message}", ex.InnerException);
                }
                else
                {
                    throw new Exception($"Could not add user to the database: {ex.Message}", ex);
                }
            }
        }


            public async Task<Users> UpdateUsersAsync(Users Users)
        {
            _context.Set<Users>().Update(Users);
            await _context.SaveChangesAsync();
            return Users;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.Set<Item>().FindAsync(id);
            if (item == null) return false;

            _context.Set<Item>().Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync(); // Correct usage of async with DbContext
        }
        //public async Task<Users> GetUserByEmail(string email)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        //}


    }
}
