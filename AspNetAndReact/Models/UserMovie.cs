using System.ComponentModel.DataAnnotations;

namespace AspNetAndReact.Models
{
    public class UserMovie
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        [Display(Name = "Rating (1 - 5)")]
        public int Rating { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}