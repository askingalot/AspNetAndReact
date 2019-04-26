using System.ComponentModel.DataAnnotations;

namespace AspNetAndReact.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public bool IsUserFavorite { get; set; }
    }
}
