using System.ComponentModel.DataAnnotations;

namespace SocietyManager.Models
{
    public class CreateSocietyDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
