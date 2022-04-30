using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SQE.Data;
using SQE.Models;
using SQE.Services;
using System;
using System.Threading.Tasks;

namespace SQE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly UserManager<ApiUser> _userManager;
        //public readonly SignInManager<ApiUser> _signInManager;
        public readonly ILogger<AccountController> _logger;
        public readonly IMapper _mapper;
        public readonly IAuthManager _authManager;

        public AccountController(
            UserManager<ApiUser> userManager, 
            //SignInManager<ApiUser> signInManager, 
            ILogger<AccountController> logger, 
            IMapper mapper,
            IAuthManager authManager)
        {
            this._userManager = userManager;
            //this._signInManager = signInManager;
            this._mapper = mapper;
            this._logger = logger;
            this._authManager = authManager;
        }
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDOT userDOT)
        {
            _logger.LogInformation($"Registration Attepmt for {userDOT.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(userDOT);
                user.UserName = userDOT.Email;
                var result = await _userManager.CreateAsync(user,userDOT.Password);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    //return BadRequest(ModelState);
                    return BadRequest(new { Status = "ok", Message = ModelState, Code = false });
                }
                await _userManager.AddToRolesAsync(user, userDOT.Roles);
                //return Accepted();
                return Accepted(new { Status = "ok", Message = "Successfully Registered", Code = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
                //return Problem(new { Status = "ok", Message = $"Something went wrong in the {nameof(Register)}", Code = false });
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDOT)
        {
            _logger.LogInformation($"Login Attepmt for {userDOT.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var result = await _signInManager.PasswordSignInAsync(userDOT.Email, userDOT.Password, false, false);
                //if (!result.Succeeded)
                //{
                //    return Unauthorized(userDOT);
                //}
                if(!await _authManager.ValidateUser(userDOT))
                {
                    //return BadRequest(new { Status = "ok", Message = ModelState, Code = false });
                    return Unauthorized(new { Status = "ok", Message = "Invalid username or password", Code = false });
                }
                //return Ok(new { Status = "ok", Message = "Successfully Registered", Code = true });
                return Accepted(new { Token = await _authManager.CreateToken(), Status = "ok", Message = "Login Success", Code = true });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

    }
}
