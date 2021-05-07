using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyManager.Models.Events
{
    public class CreateEventDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
    }
}
