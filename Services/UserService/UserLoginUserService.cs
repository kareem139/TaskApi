using codetask.Model;
using codetask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace codetask.Services.UserService
{
    public class UserLoginUserService : IUserLoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
       
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserLoginUserService(UserManager<ApplicationUser> userManager,  IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
           
           
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "userName Not Found",
                    IsSuccess = false
                };
            }

            var resul = await _userManager.CheckPasswordAsync(user, model.Password);
            if (resul)
            {

                var role = await _userManager.GetRolesAsync(user);


                var claims = new Claim[]
                {
                        new Claim("UserId",user.Id.ToString()),


                        new Claim("role",role.FirstOrDefault()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));


                var sectoken = new JwtSecurityToken
                (
                    issuer: _configuration["AuthSettings:Issuer"],
                    audience: _configuration["AuthSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );
                var token = new JwtSecurityTokenHandler().WriteToken(sectoken);
                return new UserManagerResponse
                {
                    Message = token,
                    IsSuccess = true,

                };
            }

            return new UserManagerResponse
            {
                Message = "user name or password is not correct"
            };
        }

  
    }
}
