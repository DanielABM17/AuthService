using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;
using AuthService.Entities;
using AuthService.Repository.Contract;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository
{
    public class StoreRepository : IStoreRepository
    {

        private readonly OticaContext _context;
        public StoreRepository(OticaContext context)
        {
            _context = context;
        }
        public async Task<bool> AddStore(Store store)
        {
           
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> DeleteStore(Guid storeId)
        {
            if(storeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(storeId));
            }
            var store= _context.Stores.Find(storeId);
            if(store == null)
            {
                throw new Exception("Store not found");
            }
             _context.Stores.Remove(store);
           await  _context.SaveChangesAsync();
          store = _context.Stores.Find(storeId);
          if(store != null)
          {
              return false;
          }
            return true;
        }

        public async Task<ICollection<Store>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        public Task<Store?> GetStoreById(Guid storeId)
        {
            var store = _context.Stores.FirstOrDefaultAsync(x => x.StoreId == storeId); 
        return store;
        }

        public async Task<bool> UpdateStore(Store store)
        {
            var findedStore = await GetStoreById(store.StoreId);
            if(findedStore == null)
            {
                return false;
            }
            findedStore.StoreNumber = store.StoreNumber;
            findedStore.Address = store.Address;
            
            _context.Entry(findedStore).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> AddUserToStore(User user)
        {
        var findedStore = await GetStoreById(user.StoreId);
        if(findedStore == null)
        {
            return false;
        }
        findedStore.Users.Add(user);
        _context.Entry(findedStore).State = EntityState.Modified;
        return  true;
        }
    }
}