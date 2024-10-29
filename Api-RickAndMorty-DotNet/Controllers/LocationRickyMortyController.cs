using Api_RickAndMorty_DotNet.Model;
using Api_RickAndMorty_DotNet.Service;
using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Api_RickAndMorty_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationRickyMortyController : ControllerBase
    {
        private readonly ILocationRickyMortyService _locationRickyMortyService;
        private readonly ILogger _logger;

        public LocationRickyMortyController(ILocationRickyMortyService locationRickyMortyService, ILogger<LocationRickyMortyController> logger)
        {
            _locationRickyMortyService = locationRickyMortyService;
            _logger = logger;
        }

        [HttpGet("RandomLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationRandom()
        {
            var GetLocation = await _locationRickyMortyService.GetLocationRickMorty();
            _logger.LogInformation("Location Random Gerada (Controller).");
            return StatusCode(StatusCodes.Status200OK, GetLocation);
        }

        [HttpGet("LocationById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationById(int id)
        {
            var LocationById = await _locationRickyMortyService.GetLocationRickMortyById(id);
            _logger.LogInformation($"Location By Id {id} Gerada (Controller).");
            return StatusCode(StatusCodes.Status200OK, LocationById);
        }

        [HttpGet("LocationAndCharacters/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetCharactersInLocationById(int id, int pageNumber, int pageSize)
        {
            try
            {
                var episodesCharacters = await _locationRickyMortyService.GetCharactersInLocationById(id, pageNumber, pageSize);

                if (episodesCharacters is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nenhum personagem encontrado para esta location.");
                }

                return StatusCode(StatusCodes.Status200OK, episodesCharacters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno do servidor. Log: {ex.Message}");
            }
        }
    }
}
