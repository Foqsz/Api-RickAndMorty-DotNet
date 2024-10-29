using Api_RickAndMorty_DotNet.Model;
using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;

namespace Api_RickAndMorty_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesRickyMortyController : ControllerBase
    {
        private readonly IEpisodesService _episodesService;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public EpisodesRickyMortyController(IEpisodesService episodesService, ILogger<EpisodesRickyMortyController> logger, HttpClient httpClient)
        {
            _episodesService = episodesService;
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet("EpisodesRandom")]
        public async Task<ActionResult<IEnumerable<EpisodesModel>>> GetEpisodesRandom()
        {
            var episodeRandom = await _episodesService.GetEpisodesRandom();
            _logger.LogInformation("Episódio Gerado.");
            return Ok(episodeRandom);
        }

        [HttpGet("Episodes/{id:int}")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetEpisodesById(int id)
        {
            var episodeId = await _episodesService.GetEpisodesById(id);
            _logger.LogInformation($"Gerado o episódio de id {id}");
            return Ok(episodeId);
        }

        [HttpGet("CharactersInEpisodes/{id:int}")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetCharactersByEpisodeId(int id, int pageNumber, int pageSize)
        {
            try
            { 
                var characters = await _episodesService.GetEpisodesCharactersById(id, pageNumber, pageSize);

                if (characters == null)
                {
                    return NotFound("Nenhum personagem encontrado para este episódio.");
                }

                return Ok(characters);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
