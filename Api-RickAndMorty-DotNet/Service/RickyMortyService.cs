using Api_RickAndMorty_DotNet.Service.Interface;
using System;

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
        string RickyMorty = $"https://rickandmortyapi.com/api/character/{randNum.Next(826)}";

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

    public async Task<string> GetRickyMortyById(int id)
    {
        string RickyMorty = $"https://rickandmortyapi.com/api/character/{id}";

        var getIdRickyMorty = await _httpClient.GetAsync(RickyMorty);

        if (getIdRickyMorty.IsSuccessStatusCode)
        {
            string responseIdRickyMorty = await getIdRickyMorty.Content.ReadAsStringAsync();
            _logger.LogInformation($"Character by ID {id} gerado.");
            return responseIdRickyMorty;
        }
        else
        {
            return "Not Found";
        }
    }
}
