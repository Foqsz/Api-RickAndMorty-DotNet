using Api_RickAndMorty_DotNet.Service;
using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<string>> GetLocationRandom()
        {
            string GetLocation = await _locationRickyMortyService.GetLocationRickMorty();
            _logger.LogInformation("Location Random Gerada (Controller).");
            return Ok(GetLocation);
        }

        [HttpGet("LocationById/{id:int}")] 
        public async Task<ActionResult<string>> GetLocationById(int id)
        {
            string LocationById = await _locationRickyMortyService.GetLocationRickMortyById(id);
            _logger.LogInformation($"Location By Id {id} Gerada (Controller).");
            return Ok(LocationById);
        }
    }
}
