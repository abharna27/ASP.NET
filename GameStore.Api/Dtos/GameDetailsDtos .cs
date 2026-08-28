using System.ComponentModel.DataAnnotations;

namespace gamestore.api.dtos
{
    public class GameDetailsDto
    {
        public int Id { get; set; }
       public required string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}