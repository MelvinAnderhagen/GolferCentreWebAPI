using GolferCentreWebAPI.Service.Score;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolferCentreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _service;

        public ScoreController(IScoreService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var scores = _service.GetScores();

                    if (scores == null)
                    {
                        return NotFound("Scores not found.");
                    }

                    return Ok(scores);
                }

                return BadRequest("Modelstate not valid");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetScore(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var score = _service.GetScore(id);

                    if (score == null)
                    {
                        return NotFound($"Score with ID {id} not found.");
                    }

                    return Ok(score);
                }

                return BadRequest("Modelstate not valid.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }    
        }
    }
}
