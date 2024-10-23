using Api_RickAndMorty_DotNet.Model;
using Api_RickAndMorty_DotNet.Service.Interface;
using Newtonsoft.Json;

namespace Api_RickAndMorty_DotNet.Service;

public class EpisodesService : IEpisodesService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public EpisodesService(HttpClient httpClient, ILogger<EpisodesService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
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

                var episodeFiltred = JsonConvert.SerializeObject(episodeRickyMorty, Formatting.Indented);

                _logger.LogInformation($"Episódio By Id {id} Gerado");
                return episodeFiltred;
            }
            else
            {
                return "Not Found";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao buscar o episódio.");
            return "error ao buscar episódio.";
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

                var episodeFiltred = JsonConvert.SerializeObject(episodeRickyMorty, Formatting.Indented);

                _logger.LogInformation("Episódio Gerado");
                return episodeFiltred;
            }
            else
            {
                return "Not Found";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao buscar o episódio.");
            return "error ao buscar episódio.";
        }
    }
}
