using GolferCentreWebAPI.Service.Tournament;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolferCentreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _service;
        public TournamentController(ITournamentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllTournaments()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tournaments = _service.GetTournaments();

                    if (tournaments == null)
                    {
                        return NotFound("No tournaments found.");
                    }

                    return Ok(tournaments);

                }

                return BadRequest("Modelstate not valid");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTournament(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(id.ToString()))
                    {
                        return BadRequest("ID cannot be empty.");
                    }

                    var tournament = _service.GetTournament(id);

                    if (tournament == null)
                    {
                        return NotFound("Tournament not found.");
                    }
                    
                    return Ok(tournament);
                }

                return BadRequest("Modelstate is not valid.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

    }
}
