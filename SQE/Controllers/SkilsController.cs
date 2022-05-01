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
    public class SkillsController : ControllerBase
    {
        public readonly UserManager<ApiUser> _userManager;
        public readonly IUnitOfWork _unitOfWork;
        //public readonly SignInManager<ApiUser> _signInManager;
        public readonly ILogger<SkillsController> _logger;
        public readonly IMapper _mapper;
        public readonly IAuthManager _authManager;

        public SkillsController(
            UserManager<ApiUser> userManager,
            //SignInManager<ApiUser> signInManager, 
            ILogger<SkillsController> logger,
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
        [Authorize]
        [HttpGet("getSkils")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getSkils()
        {

            _logger.LogInformation($"Get Skils Details Attepmt");
            try
            {
                var personalDetails = await _unitOfWork.Skills.GetAll(s=>s.ActiveStatus == true);
                var result = _mapper.Map<Skills>(personalDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getSkils)}");
                return Problem($"Something went wrong in the {nameof(getSkils)}", statusCode: 500);
            }

        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("createSkils")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createSkils([FromBody] CreateSkillsDOT createSkilsDOT)
        {
            //var username = HttpContext.User.Identity.Name;
            //var _user = await _userManager.FindByNameAsync(username);
            createSkilsDOT.ActiveStatus = true;


            _logger.LogInformation($"Create Skils Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var skils = _mapper.Map<Skills>(createSkilsDOT);
                await _unitOfWork.Skills.Insert(skils);
                await _unitOfWork.Save();
                return Ok(new { Status = "ok", Message = "Successfully Registered", Code = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(createSkils)}");
                return Problem($"Something went wrong in the {nameof(createSkils)}", statusCode: 500);
            }
        }
    }
}
