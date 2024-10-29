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
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationRandom()
        {
            var GetLocation = await _locationRickyMortyService.GetLocationRickMorty();
            _logger.LogInformation("Location Random Gerada (Controller).");
            return Ok(GetLocation);
        }

        [HttpGet("LocationById/{id:int}")] 
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationById(int id)
        {
            var LocationById = await _locationRickyMortyService.GetLocationRickMortyById(id);
            _logger.LogInformation($"Location By Id {id} Gerada (Controller).");
            return Ok(LocationById);
        }

        [HttpGet("LocationAndCharacters/{id:int}")]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetCharactersInLocationById(int id, int pageNumber, int pageSize)
        {
            try
            {
                var episodesCharacters = await _locationRickyMortyService.GetCharactersInLocationById(id, pageNumber, pageSize);


                if (episodesCharacters is null)
                {
                    return NotFound("Nenhum personagem encontrado para esta location.");
                }

                return Ok(episodesCharacters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
