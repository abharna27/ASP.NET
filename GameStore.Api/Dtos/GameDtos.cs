using System.ComponentModel.DataAnnotations;

namespace gamestore.api.dtos
{
    public class GameDto
    {
        public int Id { get; set; }
       public required string name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}