
using AuthService.Entities;
using AuthService.Entities.Dtos;
using AuthService.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{   [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
    
    public StoreController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddStore(StoreCreateDto storeCreate)
    {
        try
        {
             var store = new Store{
                Address = storeCreate.Address,
                StoreNumber = storeCreate.StoreNumber
             };
            var result = await _storeRepository.AddStore(store);
            if (result == false)
            {
                return BadRequest("Store not added");
            }
            return Ok("Store added successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{storeId}")]
    public async Task<IActionResult> GetStoreById(Guid storeId)
    {
        try
        {
            var store = await _storeRepository.GetStoreById(storeId);
            return Ok(store);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("stores")]
    public async Task<IActionResult> GetStores()
    {
        try
        {
            var stores = await _storeRepository.GetStores();
            return Ok(stores);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateStore(Store store)
    {
        try
        {
           
            var result = await _storeRepository.UpdateStore(store);
            if (result == false)
            {
                return BadRequest("Store not found");
            }
            return Ok("Store updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteStore(Guid storeId)
    {
        try
        {
            var result = await _storeRepository.DeleteStore(storeId);
            if (result == false)
            {
                return BadRequest("Store not found");
            }
            return Ok("Store deleted successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("addUser")]
    public async Task<IActionResult> AddUserToStore(User user)
    {
        try
        {
            var result = await _storeRepository.AddUserToStore(user);
            if (result == false)
            {
                return BadRequest("User not added to store");
            }
            return Ok("User added to store successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    
    }
}
}