using GolferCentreWebAPI.DTO.Golfer;
using GolferCentreWebAPI.Models;
using GolferCentreWebAPI.Service.Golfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GolferCentreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolferController : ControllerBase
    {
        private readonly IGolferService _service;
        public GolferController(IGolferService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetGolfers()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var golfers = _service.GetAllGolfers();

                    if (golfers == null)
                    {
                        return NotFound("Golfers not found.");
                    }

                    return Ok(golfers);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGolfer(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(id.ToString()))
                    {
                        return NotFound("Golfer ID cannot be Empty.");
                    }

                    return Ok(_service.GetGolfer(id));
                }

                return BadRequest("Modelstate not valid.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateGolfer([FromBody] CreateGolferDTO createGolferDTO)
        {
            // Validate the DTO
            if (createGolferDTO == null)
            {
                return BadRequest("Golfer data cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(createGolferDTO.FirstName) || string.IsNullOrWhiteSpace(createGolferDTO.LastName))
            {
                return BadRequest("FirstName and LastName are required.");
            }

            // Try to create the golfer
            var isCreated = _service.CreateGolfer(createGolferDTO);

            if (isCreated)
            {
                return CreatedAtAction(nameof(CreateGolfer), new { firstName = createGolferDTO.FirstName, lastName = createGolferDTO.LastName }, createGolferDTO);
            }
            else
            {
                return Conflict($"Golfer with name {createGolferDTO.FirstName} {createGolferDTO.LastName} already exists.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveGolfer(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Golfer ID cannot be empty.");
                }

                // Attempt to delete the golfer
                var isDeleted = _service.DeleteGolfer(id);

                if (isDeleted)
                {
                    return Ok($"Golfer with ID {id} successfully removed."); // Return Ok
                }
                else
                {
                    return NotFound($"Golfer with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during deletion: {ex.Message}");
                return StatusCode(500, "Internal server error"); // Return 500 on unexpected errors
            }
        }


    }
}
