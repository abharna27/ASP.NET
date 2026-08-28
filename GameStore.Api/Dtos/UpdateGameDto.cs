using System.ComponentModel.DataAnnotations;

namespace gamestore.api.dtos
{
    public record UpdateGameDto
    {
        [StringLength(10)] public required string Name { get; set; }       
       [Range(0, 100)] public decimal Price { get; set; }
        public int GenreId { get; set; }        
        public DateOnly ReleaseDate { get; set; }
    }
    }
