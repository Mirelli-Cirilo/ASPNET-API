using GameStoreApi;
using GameStoreApi.Dto;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndPoints();
app.Run();
