using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Services.UserServices;

namespace WebApi.Controllers
{
    [Route("api/users/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> GetUser(Guid id)
        {
            try
            {


                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserModel>> AddUser(UserModel newUser)
        {
            try
            {
                var addedUser = await _userService.AddUser(newUser);

                return Ok(addedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserModel>> UpdateUser(UserModel updatedUser)
        {
            try
            {
                var result = await _userService.UpdateUser(updatedUser);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.RemoveUser(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<SignUpResponseModel>> SingUp(SignUpRequestModel model)
        {
            try
            {
                return await _userService.SignUp(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult> ConfirmUserEmail(EmailConfirmationRequestModel model)
        {
            try
            {
                await _userService.ConfirmUserEmail(model);

                return Ok("Email successfully confirmed!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<SignInResponseModel>> SignIn(SignInRequestModel model)
        {
            try
            {
               return await _userService.SignIn(model);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
