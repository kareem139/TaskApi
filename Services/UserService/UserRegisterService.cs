using codetask.Model;
using codetask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codetask.Services.UserService
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        

        public UserRegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
           
        }
        public async Task<UserManagerResponse> RegisterUserAsync (RegisterViewModel models)
        {
            if (models == null)
            {
                throw new NullReferenceException("models is null");
            }

            if (models.Password != models.ConfirmPassword)
            {

                return new UserManagerResponse
                {
                    Message = "password not match",
                    IsSuccess = false
                };
            }

            models.Role = "Customer";
            var applicationUser = new ApplicationUser
            {

                Email = models.Email,
                UserName = models.UserName,
                FullName = models.FullName,
                Address = models.Address,
                Gender = models.Gender,
                

            };
            var result = await _userManager.CreateAsync(applicationUser, models.Password);
            await _userManager.AddToRoleAsync(applicationUser, models.Role);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "create success",
                    IsSuccess = true
                };
            }
            else
            {
                return new UserManagerResponse
                {
                    Message = "not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };

            }
        }
    }
}
