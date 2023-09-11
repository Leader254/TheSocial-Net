using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMessage_Bus;
using TheSocial_Auth.Models;
using TheSocial_Auth.Models.Dtos;
using TheSocial_Auth.Services.IService;

namespace TheSocial_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ResponseDto _response;
        private readonly IUserInterface _userService;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _imessageBus;
        public UserController(IUserInterface userService, IConfiguration configuration, IMessageBus imessage)
        {
            _userService = userService;
            _response = new ResponseDto();
            _configuration = configuration;
            _imessageBus = imessage;
        }
        //register user
        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerRequestDto)
        {
            var errorMessage = await _userService.RegisterUser(registerRequestDto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                //error
                _response.Success = false;
                _response.Message = errorMessage;

                return BadRequest(_response);
            }
            //success and send to message bus
            var queueName = _configuration.GetSection("QueuesandTopic:RegisterUser").Get<string>();
            var message = new UserMessage()
            {
                Email = registerRequestDto.Email,
                Name = registerRequestDto.Name
            };
            await _imessageBus.PublishMessage(message, queueName);
            _response.Message = "User Created Successfully";
            return Ok(_response);
        }

        //login user
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto)
        {
            var result = await _userService.LoginUser(loginRequestDto);
            if (result.User == null)
            {
                _response.Success = false;
                _response.Message = "Invalid Credentials";
                return Unauthorized(_response);
            }
            _response.Data = result;
            return Ok(_response);
        }

        //assign role
        [HttpPost("assignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(RegisterUserDto registerUserDto)
        {
            var result = await _userService.AssignRole(registerUserDto.Email, registerUserDto.Role);
            if (!result)
            {
                _response.Success = false;
                _response.Message = "Role was not assigned";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            _response.Success = true;
            _response.Data = result;
            _response.Message = "Role assigned successfully";
            return Ok(_response);
        }

    }
}
