using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models
{
    public class Games
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}   