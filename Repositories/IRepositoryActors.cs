using MoviesAPI.Models;

namespace MoviesAPI.Repositories
{
    public interface IRepositoryActors
    {
        Task<int> Create(Actor actor);
        Task<List<Actor>> GetActors();
        Task<Actor?> GetActor(int id);
        Task<bool> ActorExists(int id);
        Task UpdateActor(Actor actor);
        Task DeleteActor(int id);
    }
}
