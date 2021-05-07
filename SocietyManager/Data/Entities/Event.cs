using System;
using System.Collections.Generic;

namespace SocietyManager.Data.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public ICollection<SocietyEvent> Societies { get; set; }
    }
}
