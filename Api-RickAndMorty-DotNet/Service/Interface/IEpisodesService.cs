namespace Api_RickAndMorty_DotNet.Service.Interface
{
    public interface IEpisodesService
    {
        Task<string> GetEpisodesRandom();
        Task<string> GetEpisodesById(int id);
    }
}
