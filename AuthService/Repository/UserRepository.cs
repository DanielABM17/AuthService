using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;
using AuthService.Entities;
using AuthService.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository
{
    public class UserRepository(OticaContext context) : IUserRepository
    {
        private readonly OticaContext _context = context;

        public async Task<bool> AddUser(User user)
        {
          await  _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
           
              return true;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {    var user = await GetUserById(userId);
        if(user == null){
            return false;
        }
             _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserById(Guid userId)
        { var user = await _context.Users.FindAsync(userId);
        if(user == null){
            throw new Exception("User not found");
        }
           return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=> x.Username==username);
            return user;
        }

        public async Task<ICollection<User>> GetUsersByStore(Guid storeId)
        {
            return await _context.Users.Where(x => x.StoreId == storeId).ToListAsync(); 
        }

        public async Task<bool> UpdateUser(User user)
        {
            var findedUser = await GetUserById(user.UserId);
            if(findedUser == null)
            {
                return false;
            }
            findedUser.Name = user.Name;
            findedUser.Username = user.Username;
            findedUser.Password = user.Password;
            findedUser.Role = user.Role;
            findedUser.StoreId = user.StoreId;
            
             _context.Entry(findedUser).State = EntityState.Modified;
             await _context.SaveChangesAsync(); 
             return true;
        }
    }
}