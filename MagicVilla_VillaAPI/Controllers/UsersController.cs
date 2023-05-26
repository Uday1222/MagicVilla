using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly APIResponse _response;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new APIResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await this._userRepository.Login(model);
            if(loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                this._response.IsSuccess = false;
                this._response.StatusCode = HttpStatusCode.BadRequest;
                List<string> errors = new List<string>();
                errors.Add("UserName or Password is incorrect");
                this._response.ErrorMessages = errors;
                return BadRequest(_response);
            }

            this._response.IsSuccess = true;
            this._response.StatusCode = HttpStatusCode.OK;
            this._response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            var isUnique = await this._userRepository.IsUserUnique(model.UserName);
            if(!isUnique)
            {
                this._response.IsSuccess = false;
                this._response.StatusCode = HttpStatusCode.BadRequest;
                this._response.ErrorMessages.Add("UserName already exists");
                return BadRequest(_response);
            }
            var user = await this._userRepository.Register(model);
            if(user == null)
            {
                this._response.IsSuccess = false;
                this._response.StatusCode = HttpStatusCode.BadRequest;
                this._response.ErrorMessages.Add("Error while Registering");
                return BadRequest(_response);
            }

            this._response.IsSuccess = true;
            this._response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}
