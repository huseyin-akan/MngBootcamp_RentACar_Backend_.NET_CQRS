using Application.Services.UserService;
using Core.Security.Entities;
using Core.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthService: IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<bool> UserExists(string email)
        { 
            return await _userService.GetByMail(email) != null;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = await _tokenHelper.CreateToken(user, claims);
            return accessToken;
        }
    }

}
