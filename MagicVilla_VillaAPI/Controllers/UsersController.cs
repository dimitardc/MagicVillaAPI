using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var LoginResponse = await _userRepository.Login(model);
            if (LoginResponse.User == null|| string.IsNullOrEmpty(LoginResponse.Token))
            {
                _response.IsSuccess = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.Result = LoginResponse;
            _response.StatusCodes = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool isUnique = _userRepository.IsUniqueUser(model.UserName);
            if (!isUnique)
            {
                _response.IsSuccess = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(model);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.Result = user;
            _response.StatusCodes = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response); 
        }
    }
}
