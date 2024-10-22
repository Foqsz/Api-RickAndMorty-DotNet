﻿using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api_RickAndMorty_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RickyMortyController : ControllerBase
    {
        private readonly IRickyMortyService _rickyMortyService;
        private readonly ILogger _logger;

        public RickyMortyController(IRickyMortyService rickyMortyService, ILogger<RickyMortyController> logger)
        {
            _rickyMortyService = rickyMortyService;
            _logger = logger;
        }

        [HttpGet("RandomCharacter")]
        public async Task<ActionResult<string>> GetCharacterRandom()
        {
            string GetCharacter = await _rickyMortyService.GetRickyMortyRandom();
            return Ok(GetCharacter);
        }

        [HttpGet("CharacterById/{id:int}")]
        public async Task<ActionResult<string>> GetCharacterById(int id)
        {
            string GetCharacterById = await _rickyMortyService.GetRickyMortyById(id);
            return Ok(GetCharacterById);
        } 
    }
}