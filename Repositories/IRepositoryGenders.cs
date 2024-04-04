using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Repositories
{
    public interface IRepositoryGenders
    {
        Task<int> Create(Gender gender);
        Task<List<Gender>> GetGenders();
        Task<Gender?> GetGender(int id);
        Task<bool> GenderExists(int id);
        Task UpdateGender(Gender gender);
        Task DeleteGender(int id);
    }
}
