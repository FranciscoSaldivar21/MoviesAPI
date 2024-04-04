using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Repositories
{
    public class RepositoryGenders : IRepositoryGenders
    {
        private readonly ApplicationDBContext _dbContext;

        public RepositoryGenders(ApplicationDBContext context)
        {
            _dbContext = context;
        }
        public async Task<int> Create(Gender gender)
        {
            _dbContext.Add(gender);
            await _dbContext.SaveChangesAsync();
            return gender.Id;  
        }

        public async Task DeleteGender(int id)
        {
            await _dbContext.Genders.Where(gender => gender.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> GenderExists(int id)
        {
            return await _dbContext.Genders.AnyAsync(gender => gender.Id == id);
        }

        public async Task<Gender?> GetGender(int id)
        {
            return await _dbContext.Genders.FirstOrDefaultAsync(gender => gender.Id == id);
        }

        public async Task<List<Gender>> GetGenders()
        {
            //Ordenar registros por nombre
            return await _dbContext.Genders.OrderBy(gender => gender.Name).ToListAsync();
        }

        public async Task UpdateGender(Gender gender)
        {
            _dbContext.Update(gender);
            await _dbContext.SaveChangesAsync();
        }
    }
}
