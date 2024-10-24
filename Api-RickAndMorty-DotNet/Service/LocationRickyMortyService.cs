﻿using Api_RickAndMorty_DotNet.Model;
using Api_RickAndMorty_DotNet.Service.Interface;
using Newtonsoft.Json;

namespace Api_RickAndMorty_DotNet.Service
{
    public class LocationRickyMortyService : ILocationRickyMortyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public LocationRickyMortyService(HttpClient httpClient, ILogger<LocationRickyMortyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<string> GetLocationRickMorty()
        {
            Random randNum = new Random();
            string apiUrl = $"https://rickandmortyapi.com/api/location/{randNum.Next(1, 126)}";

            try
            {
                var locationResponse = await _httpClient.GetAsync(apiUrl);

                if (locationResponse.IsSuccessStatusCode)
                {
                    string locationJsonResponse = await locationResponse.Content.ReadAsStringAsync();

                    var locationRickyMorty = JsonConvert.DeserializeObject<LocationModel>(locationJsonResponse);

                    string locationFiltered = JsonConvert.SerializeObject(locationRickyMorty, Formatting.Indented);

                    _logger.LogInformation("Location gerada.");
                    return locationFiltered;
                }
                else
                {
                    _logger.LogWarning("Falha ao buscar a location");
                    return "Not Found";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao buscar a location");
                return "error ao buscar location";
            }
        }

        public async Task<string> GetLocationRickMortyById(int id)
        {
            string apiUrl = $"https://rickandmortyapi.com/api/location/{id}";

            try
            {
                var locationResponseId = await _httpClient.GetAsync(apiUrl);

                if (locationResponseId.IsSuccessStatusCode)
                {
                    string locationId = await locationResponseId.Content.ReadAsStringAsync();

                    var locationRickyMorty = JsonConvert.DeserializeObject<LocationModel>(locationId);

                    string locationJsonFiltred = JsonConvert.SerializeObject(locationRickyMorty, Formatting.Indented);

                    _logger.LogInformation($"Location Id {id} gerada.");
                    return locationJsonFiltred;
                }
                else
                {
                    _logger.LogWarning($"Falha ao buscar a location id {id}");
                    return "Not Found";
                }
            }
            catch (Exception eX)
            {
                _logger.LogError($"Erro ao buscar a location id {id}");
                return $"Erro ao buscar a location {eX.Message}";
            }
        }
    }
}
