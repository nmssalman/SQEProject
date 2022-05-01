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
        [HttpGet("getPersonalDetails/{User_Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getPersonalDetails(string User_Id)
        {
           
            _logger.LogInformation($"Get Personal Details Attepmt");
            try
            {
                var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.ApiUserId == User_Id && x.ActiveStatus == true);
                var result = _mapper.Map<PersonalDetailsDOT>(personalDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(getPersonalDetails)}", statusCode: 500);
            }

        }
            //[Authorize]
        [Authorize]
        [HttpPost("createPersonalDetails")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createPersonalDetails([FromBody] CreatePersonalDetailsDOT createPersonalDetailsDOT)
        {
            //var username = HttpContext.User.Identity.Name;
            //var _user = await _userManager.FindByNameAsync(username);
            createPersonalDetailsDOT.ActiveStatus = true;


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
        [Authorize]
        [HttpPut("updatePersonalDetails/{User_Id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> updatePersonalDetails(string User_Id, [FromBody] UpdatePersonalDetailsDOT updatePersonalDetailsDOT)
        {
            updatePersonalDetailsDOT.ActiveStatus = true;


            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalInfor = await _unitOfWork.PersonalDetails.Get(s => s.ApiUserId == User_Id);
                if (personalInfor == null)
                {
                    _logger.LogError($"Invalid update attempt {nameof(updatePersonalDetails)}");
                    return BadRequest("Submited data is invalid");
                }
                _mapper.Map(updatePersonalDetailsDOT, personalInfor);
                _unitOfWork.PersonalDetails.Update(personalInfor);
                await _unitOfWork.Save();
                return Ok(new { Status = "ok", Message = "Successfully Updated", Code = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(createPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(createPersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpPut("deletePersonalDetails/{User_Id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> deletePersonalDetails(string User_Id)
        {
            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalInfor = await _unitOfWork.PersonalDetails.Get(s => s.ApiUserId == User_Id);
                personalInfor.ActiveStatus = false;
                if (personalInfor == null)
                {
                    _logger.LogError($"Invalid delete attempt {nameof(deletePersonalDetails)}");
                    return BadRequest("Submited data is invalid");
                }
                //_mapper.Map(updatePersonalDetailsDOT, personalInfor);
                _unitOfWork.PersonalDetails.Update(personalInfor);
                await _unitOfWork.Save();
                return Ok(new { Status = "ok", Message = "Successfully Deleted", Code = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(deletePersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(deletePersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpPost("createPersonalSkils")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createPersonalSkils([FromBody] CreatePersonalSkilsDOT createPersonalSkilsDOT)
        {
            //var username = HttpContext.User.Identity.Name;
            //var _user = await _userManager.FindByNameAsync(username);
            //createPersonalDetailsDOT.ActiveStatus = true;


            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.ApiUserId == createPersonalSkilsDOT.ApiUserId && x.ActiveStatus == true);
                if (personalDetails == null)
                {
                    return BadRequest("Please create personal details first");
                }
                createPersonalSkilsDOT.PersonalDetailsId = personalDetails.Id;
                createPersonalSkilsDOT.ActiveStatus = true;
                var personalSkils = _mapper.Map<PersonalSkils>(createPersonalSkilsDOT);
                await _unitOfWork.PersonalSkils.Insert(personalSkils);
                await _unitOfWork.Save();
                return Ok(new { Status = "ok", Message = "Successfully Registered", Code = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(createPersonalSkils)}");
                return Problem($"Something went wrong in the {nameof(createPersonalSkils)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpPut("deletePersonalSkils/{User_Id}/{Skil_Id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> deletePersonalSkils(string User_Id, int Skil_Id)
        {
            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.ApiUserId == User_Id && x.ActiveStatus == true);
                if (personalDetails == null)
                {
                    return BadRequest("Please create personal details first");
                }

                var personalInfor = await _unitOfWork.PersonalSkils.Get(s => s.PersonalDetailsId == personalDetails.Id && s.SkilsId == Skil_Id);
                personalInfor.ActiveStatus = false;
                if (personalInfor == null)
                {
                    _logger.LogError($"Invalid delete attempt {nameof(deletePersonalDetails)}");
                    return BadRequest("Submited data is invalid");
                }
                //_mapper.Map(updatePersonalDetailsDOT, personalInfor);
                _unitOfWork.PersonalSkils.Update(personalInfor);
                await _unitOfWork.Save();
                return Ok(new { Status = "ok", Message = "Successfully Deleted", Code = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(deletePersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(deletePersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpGet("getPersonalSkils/{User_Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getPersonalSkils(string User_Id)
        {

            _logger.LogInformation($"Get Personal Details Attepmt");
            try
            {
                var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.ApiUserId == User_Id && x.ActiveStatus == true);
                var personalskils = await _unitOfWork.PersonalSkils.Get(s => s.PersonalDetailsId == personalDetails.Id && s.ActiveStatus == true, new List<string> { "Skilss" });

                var result = _mapper.Map<PersonalSkilsDOT>(personalskils);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(getPersonalDetails)}", statusCode: 500);
            }

        }
    }
}
