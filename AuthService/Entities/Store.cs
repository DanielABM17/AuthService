using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Entities
{
    public class Store
    {
        public Guid StoreId { get; set; } = Guid.NewGuid();
        [MaxLength(6)]
        public required string StoreNumber { get; set; }
        [MaxLength(50)] 
        public required string Address { get; set; }
        public ICollection<User?> Users { get; set; } = new List<User?>();
    
    }
}