﻿using Api_RickAndMorty_DotNet.Service.Interface; 
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Gera e retorna characters do Rick and Morty aleatórios. ",
                  Description = "Este endpoint gera uma lista de characters do Ricky And Morty aleatórios.")]
        public async Task<ActionResult<string>> GetCharacterRandom()
        {
            string GetCharacter = await _rickyMortyService.GetRickyMortyRandom();
            _logger.LogInformation("Character Random Gerado (Controller).");
            return StatusCode(StatusCodes.Status200OK, GetCharacter);
        }

        /// <summary>
        /// Gerar um character do Ricky And Morty pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o ID escolhido</returns>
        [HttpGet("CharacterById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Gera e retorna characters do Rick and Morty de acordo com o ID informado. ",
                  Description = "Este endpoint gera uma lista de characters do Ricky And Morty de acordo com o ID informado.")]
        public async Task<ActionResult<string>> GetCharacterById(int id)
        {
            string GetCharacterById = await _rickyMortyService.GetRickyMortyById(id);
            _logger.LogInformation($"Character By Id {id} Gerado (Controller).");
            return StatusCode(StatusCodes.Status200OK, GetCharacterById);
        } 

    }
}
