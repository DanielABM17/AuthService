using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Entities;

namespace AuthService.Repository.Contract
{
    public interface IUserRepository
    {
        public Task<User?> GetUserById(Guid userId);
        public Task<ICollection<User>> GetUsersByStore(Guid storeId);
         public Task<bool> AddUser(User user);
        public Task<bool> UpdateUser(User user);
        public Task<bool> DeleteUser(Guid userId);

        public Task<User?> GetUserByUsername(string username);
    }
}