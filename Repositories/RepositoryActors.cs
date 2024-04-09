using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Repositories
{
    public class RepositoryActors : IRepositoryActors
    {
        private readonly ApplicationDBContext _dbContext;
        public RepositoryActors(ApplicationDBContext context) 
        {
            _dbContext = context;
        }
        public async Task<bool> ActorExists(int id)
        {
            var found = await _dbContext.Actors.AnyAsync(a => a.Id == id);
            return found;
        }

        public async Task<int> Create(Actor actor)
        {
            _dbContext.Actors.Add(actor);
            await _dbContext.SaveChangesAsync();
            return actor.Id;
        }

        public async Task DeleteActor(int id)
        {
            await _dbContext.Actors.Where(a => a.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Actor?> GetActor(int id)
        {
            //Evita que se almacene en memoria el registro recuperado
            return await _dbContext.Actors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Actor>> GetActors()
        {
            return await _dbContext.Actors.ToListAsync();
        }

        public async Task UpdateActor(Actor actor)
        {
            _dbContext.Update(actor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
