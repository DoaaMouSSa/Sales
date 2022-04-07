using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dto.Dto
{
    public class dtoLogin
    {
        [Required]
        public string user_name { get; set; }
        [Required]
        public string password { get; set; }
    }
}
