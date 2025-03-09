
using AuthService.Entities;
using AuthService.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using AuthService.Utilities;
using AuthService.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;


namespace AuthService.Controllers
{   [Authorize(Roles ="Admin,Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        private readonly IStoreRepository _storeRepository;
       

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IConfiguration configuration, IStoreRepository storeRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
            _storeRepository = storeRepository;
            
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId){
            try
            {
                var user = await _userRepository.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddUser(UserCreateDto userCreate){
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid model");
                }
 var hashedPassword= BCrypt.Net.BCrypt.HashPassword(userCreate.Password);
                var user = new User
                {
                   
                    Name = userCreate.Name,
                    Username = userCreate.Username,
                    Password = hashedPassword,
                    StoreNumber = userCreate.StoreNumber
                };
                var result = await _userRepository.AddUser(user);
                if( result== false)
                {
                    return BadRequest("User not added");
                }
               var resultAdd=await _storeRepository.AddUserToStore(user);
               if(!resultAdd){
                     return BadRequest("User not added to store");
               }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
[AllowAnonymous]
  [HttpPost("Login")]
   public async Task<IActionResult> Login(UserLoginDto userLogin){
    try
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Invalid model");
        }
        var user = await _userRepository.GetUserByUsername(userLogin.Username);
        if(user == null)
        {
            return Unauthorized("User not found");
        }
        if(!BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
        {
            return Unauthorized("Invalid credentials");
        }
        if (!user.IsActive)
        {
            return Unauthorized("User is not active");
        }
        var token = TokenService.GenerateToken(user, _configuration);
        return Ok(new {token});
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }

  }


  [HttpGet("all/Users")]
  public async Task<IActionResult> GetUsersByStore(Guid storeId){
      try
      {
          var users = await _userRepository.GetUsersByStore(storeId);
          return Ok(users);
      }
      catch (Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  [HttpPatch("update/User")]
  public async Task<IActionResult> UpdateUser(UpdateUserDto userUpdate){
      try
      {
        var user = new User{
           
            Name = userUpdate.Name,
            Username = userUpdate.Username,
            Password = userUpdate.Password,
            Role = userUpdate.Role,
            StoreNumber = userUpdate.StoreNumber,
            IsActive=userUpdate.IsActive
        };
          if(!ModelState.IsValid)
          {
              return BadRequest("Invalid model");
        }
          var result = await _userRepository.UpdateUser(user);
          if(result == false)
          {
              return BadRequest("User not found");
          }
          return Ok("User updated successfully");
      }
      catch (Exception e)
      {
          return BadRequest(e.Message);
      }
  }
  [HttpDelete("delete/User")]
  public async Task<IActionResult> DeleteUser(Guid userId){
      try
      {
          var result = await _userRepository.DeleteUser(userId);
          if(result == false)
          {
              return BadRequest("User not found");
          }
          return Ok("User deleted successfully");
      }
      catch (Exception e)
      {
          return BadRequest(e.Message);
      }
  }
}
}