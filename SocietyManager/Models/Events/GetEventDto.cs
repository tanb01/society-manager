using System;

namespace SocietyManager.Models.Events
{
    public class GetEventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
    }
}
