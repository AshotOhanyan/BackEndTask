using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Services.ClassServices;


namespace WebApi.Controllers
{
    [Route("api/classes/[action]")]
    [ApiController]
    [Authorize]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _ClassService;

        public ClassController(IClassService ClassService)
        {
            _ClassService = ClassService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassModel>>> GetClasses()
        {
            try
            {
                var Classs = await _ClassService.GetAllClasss();

                return Ok(Classs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassModel>> GetClass(Guid id)
        {
            try
            {


                var Class = await _ClassService.GetClassById(id);
                if (Class == null)
                {
                    return NotFound();
                }

                return Ok(Class);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassModel>> AddClass(ClassModel newClass)
        {
            try
            {
                var addedClass = await _ClassService.AddClass(newClass);

                return Ok(addedClass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ClassModel>> UpdateClass(ClassModel updatedClass)
        {
            try
            {
                var result = await _ClassService.UpdateClass(updatedClass);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClass(Guid id)
        {
            try
            {
                var Class = await _ClassService.GetClassById(id);
                if (Class == null)
                {
                    return NotFound();
                }

                await _ClassService.RemoveClass(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
