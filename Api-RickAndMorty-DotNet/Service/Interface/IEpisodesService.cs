using System.Collections;

namespace Api_RickAndMorty_DotNet.Service.Interface
{
    public interface IEpisodesService
    {
        Task<IEnumerable> GetEpisodesRandom();
        Task<IEnumerable> GetEpisodesById(int id);
        Task<object> GetEpisodesCharactersById(int id, int pageNumber, int pageSize);
    }
}
