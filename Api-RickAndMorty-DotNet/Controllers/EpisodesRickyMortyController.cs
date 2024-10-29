using Api_RickAndMorty_DotNet.Model;
using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        /// Retorna uma lista de episódios aleatórios do serviço de episódios.
        /// </summary>
        /// <remarks>
        /// Este endpoint gera e retorna uma lista de episódios selecionados aleatoriamente,
        /// utilizando o serviço de episódios para buscar os dados.
        /// </remarks>
        /// <returns>Retorna uma lista de objetos <see cref="EpisodesModel"/> contendo episódios aleatórios.</returns>
        /// <response code="200">Retorna uma lista de episódios aleatórios.</response>
        [HttpGet("EpisodesRandom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Gera e retorna episódios aleatórios do Rick e Morty",
                  Description = "Este endpoint gera uma lista de episódios aleatórios para o usuário.")]
        public async Task<ActionResult<IEnumerable<EpisodesModel>>> GetEpisodesRandom()
        {
            var episodeRandom = await _episodesService.GetEpisodesRandom();
            _logger.LogInformation("Episódio Gerado.");
            return StatusCode(StatusCodes.Status200OK, episodeRandom);
        }

        /// <summary>
        /// Retorna os personagens de um episódio específico pelo ID fornecido.
        /// </summary>
        /// <param name="id">O ID do episódio a ser consultado.</param>
        /// <returns>Uma lista de <see cref="CharacterModel"/> representando os personagens do episódio solicitado.</returns>
        /// <response code="200">Retorna os personagens do episódio correspondente ao ID fornecido.</response>
        [HttpGet("Episodes/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Gera e retorna episódios do Rick e Morty conforme ID informado ",
                  Description = "Este endpoint gera uma lista de episódios de acordo com o ID desejado pelo usuário.")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetEpisodesById(int id)
        {
            var episodeId = await _episodesService.GetEpisodesById(id);
            _logger.LogInformation($"Gerado o episódio de id {id}");
            return StatusCode(StatusCodes.Status200OK, episodeId);
        }

        /// <summary>
        /// Retorna uma lista paginada de personagens para um episódio específico, identificado pelo ID do episódio.
        /// </summary>
        /// <param name="id">O ID do episódio para o qual os personagens devem ser consultados.</param>
        /// <param name="pageNumber">O número da página atual para paginação dos resultados.</param>
        /// <param name="pageSize">O número de personagens por página.</param>
        /// <returns>Uma lista de objetos <see cref="CharacterModel"/> representando os personagens do episódio solicitado.</returns>
        /// <response code="200">Retorna uma lista paginada de personagens do episódio.</response>
        /// <response code="404">Nenhum personagem encontrado para o episódio especificado.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpGet("CharactersInEpisodes/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Gera e retorna todos os characters em um determinado episódio do Ricky And Morty.",
                  Description = "Este endpoint gera uma lista de characters que aparecem em um episódio informado.")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetCharactersByEpisodeId(int id, int pageNumber, int pageSize)
        {
            try
            {
                var characters = await _episodesService.GetEpisodesCharactersById(id, pageNumber, pageSize);

                if (characters == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nenhum personagem encontrado para este episódio.");
                }

                return StatusCode(StatusCodes.Status200OK, characters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno do servidor. Causa: {ex.Message}");
            }
        }
    }
}
