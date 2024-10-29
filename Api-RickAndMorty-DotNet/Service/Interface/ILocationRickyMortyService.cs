using System.Collections;

namespace Api_RickAndMorty_DotNet.Service.Interface
{
    public interface ILocationRickyMortyService
    {
        Task<IEnumerable> GetLocationRickMorty();
        Task<IEnumerable> GetLocationRickMortyById(int id);
        Task<object> GetCharactersInLocationById(int id, int pageNumber, int pageSize);
    }
}
