using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Entities.Enums;

namespace AuthService.Entities
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        [MaxLength(50)]
        public required string Name{ get; set; }  
        [MaxLength(20)]  
        public required string Username { get; set; }
        [PasswordPropertyText]
        [MaxLength(20)]
        [MinLength(5)]
        public required string Password { get; set; }

        public bool IsActive { get; set; } = true;  

       public Roles Role { get; set; } = Roles.NotAssigned;

       public required string StoreNumber { get; set; }
        public Guid StoreId { get; set; }   
        
        

    }
}