using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace gamestore.api.data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
    {
       public DbSet<Games> Games => Set<Games>();
       public DbSet<Genre> Genres => Set<Genre>();
    }
}