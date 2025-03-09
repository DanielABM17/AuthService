using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Entities.Dtos
{
    public record StoreCreateDto
    {
         public required string StoreNumber { get; set; }
        [MaxLength(50)] 
        public required string Address { get; set; }
    }
}