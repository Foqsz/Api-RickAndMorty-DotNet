using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_RickAndMorty_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesRickyMortyController : ControllerBase
    {
        private readonly IEpisodesService _episodesService;
        private readonly ILogger _logger;

        public EpisodesRickyMortyController(IEpisodesService episodesService, ILogger<EpisodesRickyMortyController> logger)
        {
            _episodesService = episodesService;
            _logger = logger;
        }

        [HttpGet("EpisodesRandom")]
        public async Task<ActionResult<string>> GetEpisodesRandom()
        {
            string episodeRandom = await _episodesService.GetEpisodesRandom();
            _logger.LogInformation("Episódio Gerado.");
            return Ok(episodeRandom);
        }

        [HttpGet("Episodes/{id:int}")]
        public async Task<ActionResult<string>> GetEpisodesById(int id)
        {
            string episodeId = await _episodesService.GetEpisodesById(id);
            _logger.LogInformation($"Gerado o episódio de id {id}");
            return Ok(episodeId);
        }
    }
}
