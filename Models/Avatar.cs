using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Models
{
    public class Avatar
    {
        [Key]
        public string UserId { get; set; }

        public string Base64 { get; set; }
    }
}
