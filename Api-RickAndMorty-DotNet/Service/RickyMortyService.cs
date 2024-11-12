using Api_RickAndMorty_DotNet.Service.Interface;
using Newtonsoft.Json;  
using Api_RickAndMorty_DotNet.Model;

namespace Api_RickAndMorty_DotNet.Service
{
    public class RickyMortyService : IRickyMortyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RickyMortyService> _logger;

        public RickyMortyService(HttpClient httpClient, ILogger<RickyMortyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
         
        public async Task<string> GetRickyMortyRandom()
        {
            Random randNum = new Random();
            string apiUrl = $"https://rickandmortyapi.com/api/character/{randNum.Next(1, 827)}";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Desserializa e filtra os dados
                    var character = JsonConvert.DeserializeObject<CharacterModel>(jsonResponse);

                    string filteredJson = JsonConvert.SerializeObject(character, Formatting.Indented); //retornando filtrado
                     
                    _logger.LogInformation("Character aleatório gerado.");
                    return filteredJson;
                }
                else
                {
                    _logger.LogWarning($"Falha ao buscar personagem aleatório: {response.StatusCode}");
                    return "Not Found";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter personagem aleatório: {ex.Message}");
                return "Erro ao obter dados.";
            }
        }
         
        public async Task<string> GetRickyMortyById(int id)
        {
            string apiUrl = $"https://rickandmortyapi.com/api/character/{id}";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Desserializa e filtra os dados
                    var character = JsonConvert.DeserializeObject<CharacterModel>(jsonResponse);

                    string filteredJson = JsonConvert.SerializeObject(character, Formatting.Indented);

                    _logger.LogInformation($"Character by ID {id} gerado.");
                    return filteredJson;
                }
                else
                {
                    _logger.LogWarning($"Falha ao buscar personagem com ID {id}: {response.StatusCode}");
                    return "Not Found";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter personagem por ID: {ex.Message}");
                return "Erro ao obter dados.";
            }
        }
         

    }
}