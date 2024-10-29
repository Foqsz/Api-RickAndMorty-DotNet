using Api_RickAndMorty_DotNet.Model;
using Api_RickAndMorty_DotNet.Service;
using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        /// Método para buscar locations do Ricky And Morty
        /// </summary>
        /// <returns>Retorna uma location aleatória</returns>
        [HttpGet("RandomLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Gera e retorna locations do Rick and Morty aleatórios. ",
                  Description = "Este endpoint gera uma lista de locations do Ricky And Morty aleatórios.")]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationRandom()
        {
            var GetLocation = await _locationRickyMortyService.GetLocationRickMorty();
            _logger.LogInformation("Location Random Gerada (Controller).");
            return StatusCode(StatusCodes.Status200OK, GetLocation);
        }

        /// <summary>
        /// Método para buscar locations do Ricky And Morty pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna a location correspondente ao id informado</returns>
        [HttpGet("LocationById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Gera e retorna locations do Rick and Morty de acordo com o ID desejado. ",
                  Description = "Este endpoint gera uma lista de locations do Ricky And Morty de acordo com o ID informado.")]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationById(int id)
        {
            var LocationById = await _locationRickyMortyService.GetLocationRickMortyById(id);
            _logger.LogInformation($"Location By Id {id} Gerada (Controller).");
            return StatusCode(StatusCodes.Status200OK, LocationById);
        }

        /// <summary>
        /// Método para listar todos os characters em uma determinada location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>Retorna paginado todos os characters em uma location informada</returns>
        [HttpGet("LocationAndCharacters/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Gera e retorna todos characters em uma determinada location informada. ",
                  Description = "Este endpoint gera uma lista de characters que estão em uma location informada.")]
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
