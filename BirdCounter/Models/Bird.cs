using System.ComponentModel.DataAnnotations;

namespace BirdCounter.Models
{
    public class Bird
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
    }
}