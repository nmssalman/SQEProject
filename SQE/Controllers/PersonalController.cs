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
                var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.ApiUserId == User_Id && x.ActiveStatus == true, new List<string> { "ApiUsers" } );
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
        //[HttpGet("getCretedPersonalDetails1/{Id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> getCretedPersonalDetails1(int Id)
        //{

        //    _logger.LogInformation($"Get Personal Details Attepmt");
        //    try
        //    {
        //        var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.Id == Id && x.ActiveStatus == true, new List<string> { "ApiUsers" });
        //        var objApiUser = new ApiUser
        //        {
        //            Id = personalDetails.ApiUserId
        //        };
        //        var APIUser = await _userManager.FindByIdAsync(objApiUser.Id);
        //        objApiUser.Email = APIUser.Email;
        //        objApiUser.PhoneNumber = APIUser.PhoneNumber;
        //        objApiUser.FirstName = APIUser.FirstName;
        //        objApiUser.LastName = APIUser.LastName;
        //        personalDetails.ApiUser = objApiUser;

        //        var result = _mapper.Map<PersonalDetailsDOT>(personalDetails);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went wrong in the {nameof(getPersonalDetails)}");
        //        return Problem($"Something went wrong in the {nameof(getPersonalDetails)}", statusCode: 500);
        //    }

        //}
        //[Authorize]
        //[HttpGet("{Id:int}", Name = "getCretedPersonalDetails")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> getCretedPersonalDetails(int Id)
        //{


        //    _logger.LogInformation($"Get Personal Details Attepmt");
        //    try
        //    {
        //        var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.Id == Id && x.ActiveStatus == true, new List<string> { "ApiUsers" });
        //        var objApiUser = new ApiUser
        //        {
        //            Id = personalDetails.ApiUserId
        //        };
        //        var APIUser = await _userManager.FindByIdAsync(objApiUser.Id);
        //        objApiUser.Email = APIUser.Email;
        //        objApiUser.PhoneNumber = APIUser.PhoneNumber;
        //        objApiUser.FirstName = APIUser.FirstName;
        //        objApiUser.LastName = APIUser.LastName;
        //        personalDetails.ApiUser = objApiUser;

        //        var result = _mapper.Map<PersonalDetailsDOT>(personalDetails);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went wrong in the {nameof(getPersonalDetails)}");
        //        return Problem($"Something went wrong in the {nameof(getPersonalDetails)}", statusCode: 500);
        //    }

        //}
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
                return BadRequest(new { Status = false, Message = ModelState });
            }
            try
            {
                var user = _mapper.Map<PersonalDetails>(createPersonalDetailsDOT);
                await _unitOfWork.PersonalDetails.Insert(user);
                await _unitOfWork.Save();
                //var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.Id == user.Id && x.ActiveStatus == true, new List<string> { "ApiUsers" });
                var objApiUser = new ApiUser
                {
                    Id = user.ApiUserId
                };
                await _userManager.FindByIdAsync(objApiUser.Id);

                var personal = _mapper.Map<PersonalDetailsDOT>(user);
                var result = personal;
                return Ok(result);
                //return CreatedAtRoute("getCretedPersonalDetails", new { Id = user.Id }, user);
                //return Ok(new { Status = true, Message = "Successfully Registered"});
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


            _logger.LogInformation($"Update Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Status = false, Message = ModelState });
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
                return Ok(new { Status = true, Message = "Successfully Updated" });
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
            _logger.LogInformation($"Delete Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Status = false, Message = ModelState }); ;
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
                return Ok(new { Status = true, Message = "Successfully Deleted"});
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
        public async Task<IActionResult> createPersonalSkils([FromBody] CreatePersonalSkillsDOT createPersonalSkilsDOT)
        {
            //var username = HttpContext.User.Identity.Name;
            //var _user = await _userManager.FindByNameAsync(username);
            //createPersonalDetailsDOT.ActiveStatus = true;


            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Status = false, Message = ModelState });;
            }
            try
            {
                //var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.ApiUserId == createPersonalSkilsDOT.ApiUserId && x.ActiveStatus == true);
                //if (personalDetails == null)
                //{
                //    return BadRequest("Please create personal details first");
                //}
                //createPersonalSkilsDOT.PersonalDetailsId = personalDetails.Id;
                createPersonalSkilsDOT.ActiveStatus = true;
                var personalSkils = _mapper.Map<PersonalSkills>(createPersonalSkilsDOT);
                await _unitOfWork.PersonalSkills.Insert(personalSkils);
                await _unitOfWork.Save();
                var personalSkilsDetails = await _unitOfWork.PersonalSkills.Get(x => x.PersonalDetailsId == personalSkils.PersonalDetailsId && x.ActiveStatus == true, new List<string> { "Skilss" });
                var result = _mapper.Map<PersonalSkillsDOT>(personalSkilsDetails);
                return Ok(result); //Ok(new { Status = true, Message = "Successfully Registered" });
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
                    return BadRequest(new { Status = false, Message = "Please create personal details first" });
                }

                var personalInfor = await _unitOfWork.PersonalSkills.Get(s => s.PersonalDetailsId == personalDetails.Id && s.SkilsId == Skil_Id);
                personalInfor.ActiveStatus = false;
                if (personalInfor == null)
                {
                    _logger.LogError($"Invalid delete attempt {nameof(deletePersonalDetails)}");
                    return BadRequest("Submited data is invalid");
                }
                //_mapper.Map(updatePersonalDetailsDOT, personalInfor);
                _unitOfWork.PersonalSkills.Update(personalInfor);
                await _unitOfWork.Save();
                return Ok(new { Status = true, Message = "Successfully Deleted"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(deletePersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(deletePersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpGet("getPersonalSkils/{personalDetails_Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getPersonalSkils(int personalDetails_Id)
        {

            _logger.LogInformation($"Get Personal Details Attepmt");
            try
            {
                //var personalDetails = await _unitOfWork.PersonalDetails.Get(x => x.ApiUserId == User_Id && x.ActiveStatus == true);
                var personalskils = await _unitOfWork.PersonalSkills.Get(s => s.PersonalDetailsId == personalDetails_Id && s.ActiveStatus == true, new List<string> { "Skilss" });

                var result = _mapper.Map<PersonalSkillsDOT>(personalskils);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(getPersonalDetails)}", statusCode: 500);
            }

        }
        [Authorize]
        [HttpPost("createEducation")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createEducation([FromBody] EducationDTO educationDTO)
        {
            //var username = HttpContext.User.Identity.Name;
            //var _user = await _userManager.FindByNameAsync(username);
            educationDTO.ActiveStatus = true;


            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Status = false, Message = ModelState });
            }
            try
            {
                var education = _mapper.Map<Education>(educationDTO);
                await _unitOfWork.Educations.Insert(education);
                await _unitOfWork.Save();
                var educationDetails = await _unitOfWork.Educations.Get(x => x.PersonalDetailsId == education.PersonalDetailsId && x.ActiveStatus == true, new List<string> { "PersonalDetailsList" });
                //var objApiUser = new ApiUser
                //{
                //    Id = user.ApiUserId
                //};
                //await _userManager.FindByIdAsync(objApiUser.Id);

                var result = _mapper.Map<EducationDTO>(educationDetails);
                return Ok(result);
                //return CreatedAtRoute("getCretedPersonalDetails", new { Id = user.Id }, user);
                //return Ok(new { Status = true, Message = "Successfully Registered"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(createPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(createPersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpPut("deleteEducation/{Education_Id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> deleteEducation(int Education_Id)
        {
            _logger.LogInformation($"Create education Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalDetails = await _unitOfWork.Educations.Get(x => x.Id == Education_Id && x.ActiveStatus == true);
                if (personalDetails == null)
                {
                    return BadRequest(new { Status = false, Message = "Please create education details first" });
                }
                personalDetails.ActiveStatus = false;
                //_mapper.Map(updatePersonalDetailsDOT, personalInfor);
                _unitOfWork.Educations.Update(personalDetails);
                await _unitOfWork.Save();
                return Ok(new { Status = true, Message = "Successfully Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(deletePersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(deletePersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpGet("getEducation/{PersonalDetails_Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getEducation(int PersonalDetails_Id)
        {

            _logger.LogInformation($"Get Education Details Attepmt");
            try
            {
                var personalDetails = await _unitOfWork.Educations.Get(x => x.PersonalDetailsId == PersonalDetails_Id && x.ActiveStatus == true, new List<string> { "PersonalDetailsList" });
                var result = _mapper.Map<EducationDTO>(personalDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(getPersonalDetails)}", statusCode: 500);
            }

        }
        [Authorize]
        [HttpPost("createExperience")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createExperience([FromBody] ExperienceDOT experienceDOT)
        {
            //var username = HttpContext.User.Identity.Name;
            //var _user = await _userManager.FindByNameAsync(username);
            experienceDOT.ActiveStatus = true;


            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Status = false, Message = ModelState });
            }
            try
            {
                var experience = _mapper.Map<Experience>(experienceDOT);
                await _unitOfWork.Experiences.Insert(experience);
                await _unitOfWork.Save();
                var experienceDetails = await _unitOfWork.Experiences.Get(x => x.PersonalDetailsId == experience.PersonalDetailsId && x.ActiveStatus == true, new List<string> { "PersonalDetailsList" });
                //var objApiUser = new ApiUser
                //{
                //    Id = user.ApiUserId
                //};
                //await _userManager.FindByIdAsync(objApiUser.Id);

                var result = _mapper.Map<ExperienceDOT>(experienceDetails);
                return Ok(result);
                //return CreatedAtRoute("getCretedPersonalDetails", new { Id = user.Id }, user);
                //return Ok(new { Status = true, Message = "Successfully Registered"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(createPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(createPersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpPut("deleteExperience/{Experience_Id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> deleteExperience(int Experience_Id)
        {
            _logger.LogInformation($"delete Experience Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalDetails = await _unitOfWork.Experiences.Get(x => x.Id == Experience_Id && x.ActiveStatus == true);
                if (personalDetails == null)
                {
                    return BadRequest(new { Status = false, Message = "Please create education details first" });
                }
                personalDetails.ActiveStatus = false;
                //_mapper.Map(updatePersonalDetailsDOT, personalInfor);
                _unitOfWork.Experiences.Update(personalDetails);
                await _unitOfWork.Save();
                return Ok(new { Status = true, Message = "Successfully Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(deletePersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(deletePersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpGet("getExperience/{PersonalDetails_Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getExperience(int PersonalDetails_Id)
        {

            _logger.LogInformation($"Get Education Details Attepmt");
            try
            {
                var personalDetails = await _unitOfWork.Experiences.Get(x => x.PersonalDetailsId == PersonalDetails_Id && x.ActiveStatus == true, new List<string> { "PersonalDetailsList" });
                var result = _mapper.Map<ExperienceDOT>(personalDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(getPersonalDetails)}", statusCode: 500);
            }

        }
        [Authorize]
        [HttpPost("createProfilePicture")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createProfilePicture([FromBody] ProfilePictureDTO profilePictureDTO)
        {
            //var username = HttpContext.User.Identity.Name;
            //var _user = await _userManager.FindByNameAsync(username);
            profilePictureDTO.ActiveStatus = true;


            _logger.LogInformation($"Create Personal Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Status = false, Message = ModelState });
            }
            try
            {
                var profilePicture = _mapper.Map<UserProfilePicture>(profilePictureDTO);
                await _unitOfWork.ProfilePicture.Insert(profilePicture);
                await _unitOfWork.Save();
                var profilePictureDetails = await _unitOfWork.ProfilePicture.Get(x => x.Id == profilePicture.Id && x.ActiveStatus == true, new List<string> { "PersonalDetailsList" });

                var result = _mapper.Map<ProfilePictureDTO>(profilePictureDetails);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(createPersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(createPersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpPut("deleteProfilePicture/{ProfilePicture_Id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> deleteProfilePicture(int ProfilePicture_Id)
        {
            _logger.LogInformation($"delete Experience Details Attepmt");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalDetails = await _unitOfWork.ProfilePicture.Get(x => x.Id == ProfilePicture_Id && x.ActiveStatus == true);
                if (personalDetails == null)
                {
                    return BadRequest(new { Status = false, Message = "Please create education details first" });
                }
                personalDetails.ActiveStatus = false;
                //_mapper.Map(updatePersonalDetailsDOT, personalInfor);
                _unitOfWork.ProfilePicture.Update(personalDetails);
                await _unitOfWork.Save();
                return Ok(new { Status = true, Message = "Successfully Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(deletePersonalDetails)}");
                return Problem($"Something went wrong in the {nameof(deletePersonalDetails)}", statusCode: 500);
            }
        }
        [Authorize]
        [HttpGet("getProfilePicture/{PersonalDetails_Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getProfilePicture(int PersonalDetails_Id)
        {

            _logger.LogInformation($"Get ProfilePicture Details Attepmt");
            try
            {
                var personalDetails = await _unitOfWork.ProfilePicture.Get(x => x.PersonalDetailsId == PersonalDetails_Id && x.ActiveStatus == true, new List<string> { "PersonalDetailsList" });
                var result = _mapper.Map<ProfilePictureDTO>(personalDetails);
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
