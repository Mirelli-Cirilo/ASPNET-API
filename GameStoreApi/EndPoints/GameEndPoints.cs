using GameStoreApi.Dto;
namespace GameStoreApi;

public static class GameEndPoints
{
const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDTO> games = [
        new (
            1, 
            "The Last of Us",
            "Surivor",
            219.57M,
            new DateOnly(2005, 09, 07)
        ),

        new (
            2, 
            "Life is Strange",
            "Story teller",
            79.54M,
            new DateOnly(2011, 10, 11)
        ),

        new (
            3, 
            "Android Become Human",
            "Yecnology",
            119.57M,
            new DateOnly(2007, 03, 08)
        )

    ];

    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) => 
        {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndPointName);

        // POST /games
        group.MapPost("/", (CreateGameDTO newGame) =>
        {
            GameDTO game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new {id = game.Id}, game);
            
        });

        group.MapPut("/{id}", (int id, UpdateGameDTO update) => {

            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDTO
            (
                id,
                update.Name,
                update.Genre,
                update.Price,
                update.ReleaseDate

            );

                return Results.NotFound();
            });

        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
