using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.DTOs;
using MoviesAPI.Models;
using MoviesAPI.Repositories;

namespace MoviesAPI.Endpoints
{
    public static class GendersEndpoints
    {
        public static RouteGroupBuilder MapGenders(this RouteGroupBuilder group) {
            group.MapGet("/", GetElements);

            group.MapPost("/", CreateGender);

            group.MapGet("/{id:int}", GetGenderById);

            group.MapPut("/{id:int}", UpdateGender);

            group.MapDelete("/{id:int}", DeleteGender);

            return group;
        }

        static async Task<Ok<List<ReadGenderDTO>>> GetElements(IRepositoryGenders repository)
        {
            var genders = await repository.GetGenders();
            var gendersDTO = genders.Select(gender => new ReadGenderDTO { Id = gender.Id, Name = gender.Name }).ToList();
            return TypedResults.Ok(gendersDTO);
        }


        static async Task<Results<Ok<ReadGenderDTO>, NotFound>> GetGenderById(int id, IRepositoryGenders repository)
        {
            var gender = await repository.GetGender(id);
            var genderDTO = new ReadGenderDTO() { Id = id, Name = gender!.Name };

            return gender != null ? TypedResults.Ok(genderDTO) : TypedResults.NotFound();
        }

        static async Task<Created<ReadGenderDTO>> CreateGender(CreateGenderDTO generoDTO, IRepositoryGenders repository, IOutputCacheStore cache)
        {
            var gender = new Gender() { Name = generoDTO.Name };
            var id = await repository.Create(gender);
            await cache.EvictByTagAsync("Genders-get", default);
            var readGender = new ReadGenderDTO() { Id = id, Name = generoDTO.Name };
            return TypedResults.Created($"/genders/{id}", readGender);
        }

        static async Task<Results<NoContent, NotFound>> UpdateGender(int id, CreateGenderDTO genderDTO, IRepositoryGenders repository, IOutputCacheStore cache)
        {
            if (!await repository.GenderExists(id)) return TypedResults.NotFound();

            var gender = new Gender() { Id = id, Name = genderDTO.Name };
            await repository.UpdateGender(gender);
            await cache.EvictByTagAsync("Genders-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> DeleteGender(int id, IRepositoryGenders repository, IOutputCacheStore cache)
        {
            if (!await repository.GenderExists(id)) return TypedResults.NotFound();

            await repository.DeleteGender(id);
            await cache.EvictByTagAsync("Genders-get", default);

            return TypedResults.NoContent();
        }
    }
}
