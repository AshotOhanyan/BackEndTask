using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.ClassAssignmentServices;

namespace WebApi.Controllers
{
    [Route("api/classes/[action]")]
    [ApiController]
    [Authorize]
    public class ClassAssignmentController : ControllerBase
    {
        private readonly IClassAssignmentService _classAssignmentService;

        public ClassAssignmentController(IClassAssignmentService classAssignmentService)
        {
            _classAssignmentService = classAssignmentService;
        }


        [HttpPost("{userId}/{classId}")]
        public async Task<ActionResult> AssignUserToClassAsync(Guid userId, Guid classId)
        {
            try
            {
              return  Ok(await _classAssignmentService.AssignUserToClassAsync(userId, classId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{userId}/{classId}")]
        public async Task<ActionResult> RemoveUserFromClassAsync(Guid userId, Guid classId)
        {
            try
            {
                return Ok(await _classAssignmentService.RemoveUserFromClassAsync(userId, classId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
