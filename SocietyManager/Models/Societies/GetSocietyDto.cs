using SocietyManager.Models.Events;
using System;
using System.Collections.Generic;

namespace SocietyManager.Models
{
    public class GetSocietyDto
    {
        public int SocietyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<GetStudentDto> Members { get; set; }
        public ICollection<GetEventDto> Events { get; set; }
    }
}
