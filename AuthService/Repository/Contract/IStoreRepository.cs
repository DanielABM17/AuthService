using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Entities;

namespace AuthService.Repository.Contract
{
    public interface IStoreRepository
    {
          public Task<Store?> GetStoreById(Guid storeId);
        public Task<ICollection<Store>> GetStores();
         public Task<bool> AddStore(Store store);
        public Task<bool> UpdateStore(Store store);
        public Task<bool> DeleteStore(Guid storeId);
        public Task<bool> AddUserToStore( User user);
    }
}