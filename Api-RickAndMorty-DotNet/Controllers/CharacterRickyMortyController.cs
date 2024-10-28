using Api_RickAndMorty_DotNet.Service.Interface; 
using Microsoft.AspNetCore.Mvc; 

namespace Api_RickAndMorty_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterRickyMortyController : ControllerBase
    {
        private readonly IRickyMortyService _rickyMortyService;
        private readonly ILogger _logger;

        public CharacterRickyMortyController(IRickyMortyService rickyMortyService, ILogger<CharacterRickyMortyController> logger)
        {
            _rickyMortyService = rickyMortyService;
            _logger = logger;
        }
         
        /// <summary>
        /// Gerar um character do Ricky And Morty Aleatório
        /// </summary>
        /// <returns>Retorna um Random Character</returns>
        [HttpGet("RandomCharacter")]
        public async Task<ActionResult<string>> GetCharacterRandom()
        {
            string GetCharacter = await _rickyMortyService.GetRickyMortyRandom();
            _logger.LogInformation("Character Random Gerado (Controller).");
            return Ok(GetCharacter);
        }

        /// <summary>
        /// Gerar um character do Ricky And Morty pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o ID escolhido</returns>
        [HttpGet("CharacterById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetCharacterById(int id)
        {
            string GetCharacterById = await _rickyMortyService.GetRickyMortyById(id);
            _logger.LogInformation($"Character By Id {id} Gerado (Controller).");
            return Ok(GetCharacterById);
        } 

    }
}
