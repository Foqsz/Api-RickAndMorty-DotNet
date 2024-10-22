using Api_RickAndMorty_DotNet.Service.Interface;

namespace Api_RickAndMorty_DotNet.Service;

public class RickyMortyService : IRickyMortyService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public RickyMortyService(HttpClient httpClient, ILogger<RickyMortyService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetRickyMortyRandom()
    {
        Random randNum = new Random();
        string RickyMorty = $"https://rickandmortyapi.com/api/character/{randNum.Next(500)}";

        var getRandomRickyMorty = await _httpClient.GetAsync(RickyMorty);

        if (getRandomRickyMorty.IsSuccessStatusCode)
        {
            string responseRandomRickyMorty = await getRandomRickyMorty.Content.ReadAsStringAsync();
            _logger.LogInformation("Character aleatório gerado.");
            return responseRandomRickyMorty;
        }
        else
        {
            return "Not Found";
        }
    }
}
