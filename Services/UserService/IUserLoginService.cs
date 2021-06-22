using codetask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codetask.Services.UserService
{
    public interface IUserLoginService
    {
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel models);
    }
}
