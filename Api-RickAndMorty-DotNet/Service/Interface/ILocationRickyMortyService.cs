using System.Collections;

namespace Api_RickAndMorty_DotNet.Service.Interface
{
    public interface ILocationRickyMortyService
    {
        Task<string> GetLocationRickMorty();
        Task<string> GetLocationRickMortyById(int id);
        Task<IEnumerable> GetCharactersInLocationById(int id);
    }
}
