using GolferCentreWebAPI.Service.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolferCentreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_service.GetAllCourses());
                }

                return BadRequest("Modelstate not valid");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(id.ToString()))
                    {
                        return NotFound("Course ID cannot be empty.");
                    }

                    return Ok(_service.GetCourse(id));
                }

                return BadRequest("Modelstate not valid");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }
    }
}
