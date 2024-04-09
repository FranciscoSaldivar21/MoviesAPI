using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Models;
using MoviesAPI.Repositories;
using MoviesAPI.Services;

namespace MoviesAPI.Endpoints
{
    public static class ActorEndpoints
    {
        public static RouteGroupBuilder MapActors(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:int}", GetActorById);
            group.MapGet("/", GetActors);
            group.MapPost("/", CreateActor).DisableAntiforgery();
            group.MapPut("/{id:int}", UpdateActor).DisableAntiforgery();
            group.MapDelete("/{id:int}", DeleteActor);
            return group;
        }

        public static async Task<Created<ReadActorDTO>> CreateActor(IRepositoryActors repository, [FromForm] CreateActorDTO newActor, ISaveFiles saveFile)
        {
            var actor = new Actor()
            {
                Name = newActor.Name,
                Birthdate = newActor.Birthdate,
                Picture = newActor.Picture!.FileName,
            };

            if(newActor.Picture != null)
            {
                var url = await saveFile.SaveFile(newActor.Picture, "Actors");
            }

            var id = await repository.Create(actor);
            var actorCreated = new ReadActorDTO() { Id = id, Name = newActor.Name, Birthdate = newActor.Birthdate, Picture = newActor.Picture.FileName };
            return TypedResults.Created($"/actors/{id}", actorCreated);
        }

        public static async Task<Results<Ok, NotFound>> UpdateActor(IRepositoryActors repository, CreateActorDTO actorDTO, int id)
        {
            if (await repository.ActorExists(id))
            {
                var actor = new Actor()
                {
                    Id = id,
                    Name = actorDTO.Name,
                    Birthdate = actorDTO.Birthdate,
                    Picture = actorDTO.Picture!.FileName,
                };

                await repository.UpdateActor(actor);

                return TypedResults.Ok();
            }
            return TypedResults.NotFound();
        }

        public static async Task<Results<Ok, NotFound>> DeleteActor(IRepositoryActors repository, int id)
        {
            if(!await repository.ActorExists(id)) return TypedResults.NotFound();

            await repository.DeleteActor(id);
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok<ReadActorDTO>, NotFound>> GetActorById(IRepositoryActors repository, int id)
        {
            if (!await repository.ActorExists(id)) return TypedResults.NotFound();

            var actor = await repository.GetActor(id);
            var actorDTO = new ReadActorDTO() { Id = id, Birthdate = actor!.Birthdate, Name = actor.Name, Picture = actor.Picture };

            return TypedResults.Ok(actorDTO);
        }

        public static async Task<Ok<List<ReadActorDTO>>> GetActors(IRepositoryActors repository)
        {
            var actors = await repository.GetActors();
            var gendersDTO = actors.Select(a => new ReadActorDTO() { Id = a.Id, Birthdate = a.Birthdate, Name = a.Name, Picture = a.Picture }).ToList();

            return TypedResults.Ok(gendersDTO);
        }
    }
}
