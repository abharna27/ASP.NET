using System.Security.Principal;
using gamestore.api.data;
using gamestore.api.dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string getGameEndpointName = "GetGame";
   private static readonly List <GameDto> games =[
    new GameDto { Id = 1, name = "Game 1", Price = 59.99m, Genre = "Action", ReleaseDate = new DateTime(2022, 1, 1) },
    new GameDto { Id = 2, name = "Game 2", Price = 49.99m, Genre = "Adventure", ReleaseDate = new DateTime(2022, 2, 1) },
    new GameDto { Id = 3, name = "Game 3", Price = 39.99m, Genre = "RPG", ReleaseDate = new DateTime(2022, 3, 1) }
];
 public static void MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
       // GET / games
        group.MapGet("/", () => games);


      // GET / games/{1}
    group.MapGet("/{id}", (int id) => 
   {
      var game = games.Find(games => games.Id == id);
      return game is null ? Results.NotFound() : Results.Ok(game);
    })
     .WithName(getGameEndpointName);

    // POST / games
    group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => 
    { 
        if (string.IsNullOrEmpty(newGame.Name))
        {
            return Results.BadRequest("Name is required");
        }

        Games game = new()
        {
            Name = newGame.Name,
            GenreId = newGame.GenreId,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };
         DbContext.Games.Add(game);
   return Results.CreatedAtRoute(
    getGameEndpointName,
    new { id = game.Id },
    game);});

    //put / games/{1}
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
    {
        var index = games.FindIndex(game => game.Id == id);
        if (index == -1)
        {
            return Results.NotFound();
        }
       games[index] = new GameDto
        {
            Id = id,
            name = updatedGame.Name,
            Price = updatedGame.Price,
            Genre = updatedGame.Genre,
            ReleaseDate = updatedGame.ReleaseDate.ToDateTime(new TimeOnly(0, 0))
        };
        return Results.NoContent();
    });
    // DELETE / games/{1}
    group.MapDelete("/{id}", (int id) =>
    {
        games.RemoveAll(game => game.Id == id);
        return Results.NoContent();
    });

    }
}