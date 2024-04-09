namespace MoviesAPI.Services
{
    public interface ISaveFiles
    {
        Task DeleteFile(string path, string container);
        Task<string> SaveFile(IFormFile file, string container);
        async Task<string> UpdateFile(string path, string container, IFormFile file)
        {
            await DeleteFile(path, container);
            return await SaveFile(file, container);
        }
    }
}
