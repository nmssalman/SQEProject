using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SQE.Data;
using SQE.Models;
using SQE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQE.IRepository;

namespace SQE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        public readonly UserManager<ApiUser> _userManager;
        public readonly IUnitOfWork _unitOfWork;
        //public readonly SignInManager<ApiUser> _signInManager;
        public readonly ILogger<PersonalController> _logger;
        public readonly IMapper _mapper;
        public readonly IAuthManager _authManager;

        public PersonalController(
            UserManager<ApiUser> userManager,
            //SignInManager<ApiUser> signInManager, 
            ILogger<PersonalController> logger,
            IMapper mapper,
            IAuthManager authManager,
            IUnitOfWork unitOfWork)
        {
            this._userManager = userManager;
            //this._signInManager = signInManager;
            this._mapper = mapper;
            this._logger = logger;
            this._authManager = authManager;
            this._unitOfWork = unitOfWork;
        }
        //[Authorize]
        [Authorize]
        [HttpPost("createPersonalDetails")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createPersonalDetails([FromBody] CreatePersonalDetailsDOT createPersonalDetailsDOT)
        {
            var username = HttpContext.User.Identity.Name;
            var _user = await _userManager.FindByNameAsync(username);
            createPersonalDetailsDOT.ApiUserId = _user.Id;


            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<PersonalDetails>(createPersonalDetailsDOT);
                await _unitOfWork.PersonalDetails.Insert(user);
                await _unitOfWork.Save();
                return Ok(new { Status = "ok", Message = "Successfully Registered", Code = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(createPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(createPersonalDetails)}", statusCode: 500);
            }
        }
    }
}
