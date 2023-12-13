using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeechBackend.BLL.DTO.Request;
using SpeechBackend.BLL.Services.Interfaces;
using SpeechBackend.DAL.Entity;

namespace SpeechBackend.API.Controllers
{
    [ApiController]
    [Route("AttempController")]
    public class AttempController : ControllerBase
    {
        readonly IAttempService service;
        public AttempController(IAttempService attempService)
        {
            this.service = attempService;
        }

        [HttpPost("AddAttemp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> AddAttemp([FromBody] AddAttempRequest request)
        {
            try
            {
                request.DateTime = DateTime.Now;
                await service.AddAttemp(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpGet("GetUserAttemps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult<List<Attemp>>> GetAttemps([FromQuery] int id)
        {
            try
            {
                var item = await service.GetUserAttemps(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
    }
}