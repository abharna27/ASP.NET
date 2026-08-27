using gamestore.api.data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var ConnString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(
    ConnString,
    optionsAction: options => options.UseSeeding((context, _) =>
    {
        if (!context.Set<Genre>().Any()){
            context.Set<Genre>().AddRange(
                new Genre {Name ="fightimg"},
                new Genre {Name ="RPG"},
                new Genre {Name ="Plateform"},
                new Genre {Name ="Racing"},
                new Genre {Name ="Sports"}
            );
            context.SaveChanges();
        }
    }));
    }
}

