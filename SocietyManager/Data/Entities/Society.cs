using System;
using System.Collections.Generic;

namespace SocietyManager.Data.Entities
{
    public class Society
    {
        public int SocietyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<SocietyStudent> Members { get; set; }
        public ICollection<SocietyEvent> Events { get; set; }
    }
}
