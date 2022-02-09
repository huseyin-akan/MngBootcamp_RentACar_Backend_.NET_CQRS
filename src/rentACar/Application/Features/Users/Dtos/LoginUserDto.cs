using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Dtos
{
    public class LoginUserDto
    {
        public string Email { get; set; }
        public bool LoginIsSuccessful { get; set; }
    }
}
