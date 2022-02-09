using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Dtos
{
    public class UserForLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
