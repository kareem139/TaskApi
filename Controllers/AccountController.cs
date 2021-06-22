using codetask.Services.UserService;
using codetask.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace codetask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class AccountController : ControllerBase
    {
        private readonly  IUserRegisterService _IuserRegisterService;
        private readonly IUserLoginService _IUserLoginService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUserRegisterService IuserRegisterService, IUserLoginService IUserLoginService, ILogger<AccountController> logger)
        {
            _IuserRegisterService = IuserRegisterService;
            _IUserLoginService = IUserLoginService;
            _logger = logger;


        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel models)
        {
            if (ModelState.IsValid)
            {
                var result = await _IuserRegisterService.RegisterUserAsync(models);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            else
            {
                return BadRequest("some prop not valid");
            }

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {
                var result = await _IUserLoginService.LoginUserAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            else
            {
                return BadRequest("some prop not valid");
            }

        }

    }
}
