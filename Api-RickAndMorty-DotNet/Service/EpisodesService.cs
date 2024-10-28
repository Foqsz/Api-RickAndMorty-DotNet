using Api_RickAndMorty_DotNet.Context;
using Api_RickAndMorty_DotNet.Model;
using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api_RickAndMorty_DotNet.Service;

public class EpisodesService : IEpisodesService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly AppDbContext _context;

    public EpisodesService(HttpClient httpClient, ILogger<EpisodesService> logger, AppDbContext context)
    {
        _httpClient = httpClient;
        _logger = logger;
        _context = context;
    }

    public async Task<string> GetEpisodesById(int id)
    { 
        string apiUrl = $"https://rickandmortyapi.com/api/episode/{id}";

        try
        {
            var episodeResponse = await _httpClient.GetAsync(apiUrl);

            if (episodeResponse.IsSuccessStatusCode)
            {
                string episodeJsonResponse = await episodeResponse.Content.ReadAsStringAsync();

                var episodeRickyMorty = JsonConvert.DeserializeObject<EpisodesModel>(episodeJsonResponse);

                var episodeExist = await _context.EpisodesModels.FindAsync(episodeRickyMorty.Id);

                if (episodeExist == null)
                {
                    _context.Add(episodeRickyMorty);
                    _logger.LogInformation($"Episódio By Id {id} Gerado");
                    await _context.SaveChangesAsync();
                } 

                else
                {
                    _logger.LogInformation($"Episódio Id {id} já está no banco de dados.");
                }

                var episodeFiltred = JsonConvert.SerializeObject(episodeRickyMorty, Formatting.Indented);
                   
                return episodeFiltred;
            }
            else
            {
                return "Not Found";
            }
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"Erro de atualização do banco de dados: {ex.InnerException?.Message ?? ex.Message}");
            return "Erro ao salvar episódio no banco de dados.";
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao adicionar episódio: {ex.Message}");
            return "Erro ao buscar episódio.";
        }
    }

    public async Task<string> GetEpisodesRandom()
    {
        Random randNum = new Random();
        string apiUrl = $"https://rickandmortyapi.com/api/episode/{randNum.Next(1, 51)}";

        try
        {
            var episodeResponse = await _httpClient.GetAsync(apiUrl);

            if (episodeResponse.IsSuccessStatusCode)
            {
                string episodeJsonResponse = await episodeResponse.Content.ReadAsStringAsync();

                var episodeRickyMorty = JsonConvert.DeserializeObject<EpisodesModel>(episodeJsonResponse);

                var episodeExist = await _context.EpisodesModels.FindAsync(episodeRickyMorty.Id);

                if (episodeExist == null)
                {
                    _context.Add(episodeRickyMorty);
                    _logger.LogInformation($"Episódio Gerado");
                    await _context.SaveChangesAsync();
                }

                else
                {
                    _logger.LogInformation($"Episódio Id {episodeRickyMorty.Id} já está no banco de dados.");
                }

                var episodeFiltred = JsonConvert.SerializeObject(episodeRickyMorty, Formatting.Indented); 

                return episodeFiltred;
            }
            else
            {
                return "Not Found";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar o episódio. {ex.Message}");
            return "error ao buscar episódio.";
        }
    }
}
