using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyManager.Models
{
    public class CreateStudentDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
