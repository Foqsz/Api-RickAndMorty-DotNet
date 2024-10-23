namespace Api_RickAndMorty_DotNet.Service.Interface
{
    public interface ILocationRickyMortyService
    {
        Task<string> GetLocationRickMorty();
        Task<string> GetLocationRickMortyById(int id);
    }
}
