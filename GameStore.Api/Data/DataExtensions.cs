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
        //DBContext have scoped service lifetime because :
        // 1. it ensures that new instance of dbcontext is created per request 
        // 2. DB connections are a limited and expensive resource 
        // 3. DB Context is not thread-safe . Scopped avoides to concurrency issues
        // 4. Makes it easier to manage transactions and ensure data consistency 
        // 5. Reusing a DBContext instance can lead to increase memory usage 
         builder.Services.AddScoped<GameStoreContext>();
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

