using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeechBackend.BLL.Services.Interfaces;

namespace SpeechBackend.API.Controllers
{
    [Route("SentenceController")]
    [ApiController]
    public class SentenceController : ControllerBase
    {
        readonly ISentenceService service;

        public SentenceController(ISentenceService service)
        {
            this.service = service;
        }

        [HttpGet("GetSentence")]
        [Authorize]
        public async Task<ActionResult> GetSentence(string id)
        {
            try
            {
                var sentence= await service.GetSentence(id);

                string filePath = sentence.Path;

                if (!System.IO.File.Exists(filePath))
                    return NotFound();

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                return File(fileStream, "audio/mp3"); // Застосуйте потрібний MIME-тип для вашого аудіофайлу
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSentencePath")]
        [Authorize]
        public async Task<ActionResult<string>> GetSentencePath(string id)
        {
            try
            {
                var sentence = await service.GetSentence(id);

                string filePath = sentence.Path.Trim();

                return filePath;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
